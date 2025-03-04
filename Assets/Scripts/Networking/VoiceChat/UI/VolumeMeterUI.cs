using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using TMPro;

public class VolumeMeterUI : MonoBehaviour
{
    [Header("References")]
    public Recorder recorder;         // Link your Photon Voice Recorder here
    public Image volumeFill;          // Image with Fill Method = Horizontal
    public TMP_Text screamText;       // For “Scream Detected!”

    [Header("Threshold & Sensitivity")]
    [Range(0f, 1f)]
    public float threshold = 0.7f;    // Adjust in Inspector or via UI sliders
    [Range(1f, 100f)]
    public float sensitivity = 15f;   // How strongly we amplify the level

    [Header("UI Smoothing")]
    public float smoothSpeed = 5f;    // Lerp speed for the fill amount

    private float currentFill = 0f;

    private void Update()
    {
        // Ensure recorder is valid and actually collecting audio
        if (recorder != null && recorder.LevelMeter != null)
        {
            // 1) Grab the current average amplitude from Photon Voice
            float amplitude = recorder.LevelMeter.CurrentAvgAmp;

            // 2) Optionally multiply by a sensitivity factor
            amplitude *= sensitivity;

            // 3) Clamp to [0..1] for an Image fillAmount
            amplitude = Mathf.Clamp01(amplitude);

            // 4) Smoothly interpolate our UI fill toward the new amplitude
            currentFill = Mathf.Lerp(currentFill, amplitude, Time.deltaTime * smoothSpeed);

            // 5) Assign this to the volumeFill image
            volumeFill.fillAmount = currentFill;

            // 6) Check if we have crossed our threshold for “scream”
            if (currentFill >= threshold)
            {
                screamText.gameObject.SetActive(true);
                screamText.text = "SCREAM DETECTED";
            }
            else
            {
                screamText.gameObject.SetActive(false);
            }
        }
    }
}
