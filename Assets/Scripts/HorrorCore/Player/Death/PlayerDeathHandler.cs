using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Camera Fall Settings")]
    public Transform cameraTransform;     // reference to the player's camera transform
    public Vector3 fallOffsetPosition;    // relative offset from the player’s root to simulate being on the ground
    public Vector3 fallOffsetRotation;    // euler angles to tilt the camera
    public float fallDuration = 1.0f;     // how long the fall animation takes

    private Vector3 originalCamLocalPos;
    private Quaternion originalCamLocalRot;
    private bool isFalling = false;
    private float fallTimer = 0f;

    private void OnEnable()
    {
        AIDirector.OnEnemyKilledPlayer += StartDeathSequence;
    }

    private void OnDisable()
    {
        AIDirector.OnEnemyKilledPlayer -= StartDeathSequence;
    }

    private void StartDeathSequence()
    {
        // 1) Store original camera local position/rotation
        originalCamLocalPos = cameraTransform.localPosition;
        originalCamLocalRot = cameraTransform.localRotation;

        // 2) Disable player movement here or call a method that does so
        GetComponentInParent<PlayerMovement>().enabled = false;

        // 3) Begin the fall logic
        isFalling = true;
        fallTimer = 0f;
    }

    private void Update()
    {
        if (isFalling)
        {
            fallTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fallTimer / fallDuration);

            // Lerp position
            Vector3 targetPos = originalCamLocalPos + fallOffsetPosition;
            cameraTransform.localPosition = Vector3.Lerp(originalCamLocalPos, targetPos, t);

            // Lerp rotation
            Quaternion targetRot = Quaternion.Euler(fallOffsetRotation);
            cameraTransform.localRotation = Quaternion.Slerp(originalCamLocalRot, targetRot, t);

            if (t >= 1f)
            {
                // We’ve reached the final fall position
                isFalling = false;
            }
        }
    }
}
