using UnityEngine;
using System.Collections;

public class RandomScreeching : MonoBehaviour
{
    // Array to hold your sound clips. Populate this in the Inspector.
    public AudioClip[] soundClips;

    // Time interval in seconds between playing sounds.
    public float playInterval = 8.0f;

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the game object.
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource component is attached.
        if (audioSource == null)
        {
            //Debug.LogError("AudioSource component missing from this game object.");
            return;
        }

        // Start the coroutine that plays a random sound at each interval.
        if (soundClips.Length > 0)
        {
            StartCoroutine(PlayRandomSound());
        }
        else
        {
            //Debug.LogWarning("No sound clips assigned. Please assign at least one AudioClip in the Inspector.");
        }
    }

    IEnumerator PlayRandomSound()
    {
        // Infinite loop to continue playing sounds at the set interval.
        while (true)
        {
            // Wait for the specified interval.
            yield return new WaitForSeconds(playInterval);

            // Choose a random clip from the array.
            AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];

            // Assign and play the chosen clip.
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}