using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource audioSource;         // The AudioSource component on the AudioManager
    public AudioClip footstepClip;            // The footstep sound clip
    public float footstepInterval = 0.56f;    // Interval between footsteps
    public float movementThreshold;    // Minimum velocity to consider the player moving

    // New public fields to control pitch randomization range
    public float pitchMin = 0.8f;
    public float pitchMax = 1.2f;

    private float footstepTimer;
    private Rigidbody rb;

    void Start()
    {
        footstepTimer = footstepInterval;
        // Get the Rigidbody component from the parent (the player object)
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody not found in parent hierarchy. Footstep sounds may not play correctly.");
        }
    }

    void Update()
    {
        if (IsMoving())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                // Randomize the pitch each time before playing the sound
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.PlayOneShot(footstepClip);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            // Reset timer when not moving so the footsteps sync correctly
            footstepTimer = footstepInterval;
        }
    }

    bool IsMoving()
    {
        if (rb != null)
        {
            return rb.linearVelocity.magnitude > movementThreshold;
        }
        // Fallback using input axes, though ideally the rigidbody should be found
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }
}
