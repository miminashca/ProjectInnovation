using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using TMPro;

public class SoundMeter : MonoBehaviour
{
    [Header("References")]
    public Recorder recorder;
    public Image volumeFill;
    public TMP_Text screamText;

    [Header("Threshold & Sensitivity")]
    [Range(0f, 1f)] public float threshold = 0.7f;
    [Range(1f, 100f)] public float sensitivity = 15f;

    [Header("UI Smoothing")]
    public float smoothSpeed = 5f;
    public float warningDuration = 1.5f;

    [Header("Colors")]
    public Color safeColor = Color.green;
    public Color warningColor = new Color(1f, 0.6f, 0f); // Orange
    public Color dangerColor = Color.red;

    private float currentFill = 0f;
    private float screamTimer = 0f;
    private string[] warningMessages = {
        "KEEP IT QUIET",
        "SPOTTED"

    };
    private void Start()
    {
        GameObject voiceManager = GameObject.Find("VoiceManager");

        if (voiceManager != null)
        {
            // Get the Recorder component from the VoiceManager
            recorder = voiceManager.GetComponent<Recorder>();

            if (recorder == null)
            {
                Debug.LogError("Recorder component not found on VoiceManager!");
            }
        }
        else
        {
            Debug.LogError("VoiceManager GameObject not found in the scene!");
        }
    }

    private void Update()
    {
        if (recorder != null && recorder.LevelMeter != null)
        {
            float amplitude = recorder.LevelMeter.CurrentAvgAmp * sensitivity;
            amplitude = Mathf.Clamp01(amplitude);
            currentFill = Mathf.Lerp(currentFill, amplitude, Time.deltaTime * smoothSpeed);
            volumeFill.fillAmount = currentFill;

            // Determine the color based on volume level
            if (currentFill < threshold * 0.7f)
            {
                volumeFill.color = safeColor;
            }
            else if (currentFill >= threshold * 0.7f && currentFill < threshold)
            {
                volumeFill.color = warningColor;
                screamText.gameObject.SetActive(true);
                screamText.text = warningMessages[0];
            }
            else
            {
                volumeFill.color = dangerColor;
            }

            // Scream detection logic
            if (currentFill >= threshold)
            {
                screamText.gameObject.SetActive(true);
                screamText.text = warningMessages[1];
                screamTimer = warningDuration;
            }

            if (screamTimer > 0)
            {
                screamTimer -= Time.deltaTime;
            }
            else
            {
                screamText.gameObject.SetActive(false);
            }
        }
    }
}
