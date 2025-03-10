using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOcclusion : MonoBehaviour
{
    public AudioListener listener;  // Reference to the listener (e.g., player camera)
    public float maxDistance = 30f;  // Maximum distance for audio to play at full volume
    public float occlusionThreshold = 0.5f;  // Minimum threshold for occlusion effect
    public LayerMask occlusionLayerMask;  // Layer mask for the obstacles to be checked by the raycast
    public int numberOfRays = 10;  // Number of rays to cast in the cone
    public float coneAngle = 45f;  // Angle of the cone for the raycast spread

    // New variable to scale the effect of occlusion on the low pass filter cutoff
    public float occlusionCutoffScalingFactor = 0.1f;  // Lower this value to decrease the effect of each ray

    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;
    private float originalVolume;  // The original volume of the AudioSource
    private float originalCutoffFrequency;  // The original cutoff frequency of the low pass filter

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;  // Save the original volume

        // Get or add the AudioLowPassFilter component
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        if (lowPassFilter == null)
        {
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        }

        originalCutoffFrequency = lowPassFilter.cutoffFrequency;  // Save the original cutoff frequency
    }

    void Update()
    {
        if (listener == null) return;  // Exit if there's no listener set

        // Calculate the distance between the AudioSource and the listener
        float distanceToListener = Vector3.Distance(transform.position, listener.transform.position);

        // Direction from the AudioSource to the listener
        Vector3 directionToListener = (listener.transform.position - transform.position).normalized;

        // Calculate the angle step for distributing the rays within the cone
        float angleStep = coneAngle / (numberOfRays - 1);

        int blockedRayCount = 0;

        // Cast multiple rays within a cone
        for (int i = 0; i < numberOfRays; i++)
        {
            // Calculate the direction for each ray within the cone
            float angle = -coneAngle / 2 + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 rayDirection = rotation * directionToListener;

            // Perform the raycast to detect occlusion, using the specified layer mask
            RaycastHit hit;
            bool rayBlocked = Physics.Raycast(transform.position, rayDirection, out hit, distanceToListener, occlusionLayerMask);

            // Draw individual rays in Scene view
            Color rayColor = rayBlocked ? Color.red : Color.green;
            Debug.DrawRay(transform.position, rayDirection * distanceToListener, rayColor);

            // Count the number of blocked rays
            if (rayBlocked)
            {
                blockedRayCount++;
            }
        }

        // Adjust volume and low pass filter based on occlusion
        if (blockedRayCount > 0)
        {
            // Calculate the occlusion factor based on the number of blocked rays
            float occlusionFactor = Mathf.InverseLerp(0f, numberOfRays, blockedRayCount);

            // Scale the occlusion effect to make it less drastic
            float scaledOcclusionFactor = occlusionFactor * occlusionCutoffScalingFactor;

            // Apply the occlusion effect (volume and low pass filter)
            audioSource.volume = Mathf.Lerp(0f, originalVolume, 1f - Mathf.InverseLerp(0f, maxDistance, distanceToListener) * occlusionThreshold);

            // Apply the scaled occlusion factor to lower the cutoff frequency
            lowPassFilter.cutoffFrequency = Mathf.Lerp(originalCutoffFrequency, 500f, scaledOcclusionFactor);  // Lower cutoff to muffle sound
        }
        else
        {
            // No occlusion: adjust the volume and reset the low pass filter
            audioSource.volume = Mathf.Lerp(0f, originalVolume, 1f - Mathf.InverseLerp(0f, maxDistance, distanceToListener));
            lowPassFilter.cutoffFrequency = originalCutoffFrequency;  // Reset to the original frequency when not occluded
        }
    }
}
