using UnityEngine;

public class AudioSensor : MonoBehaviour
{
    // This script can accumulate loudness from Photon Voice or footstep events
    [SerializeField] private float loudnessDecaySpeed = 1f;

    public float CurrentLoudness { get; private set; }

    private void Update()
    {
        // Example: “CurrentLoudness” decays over time if not receiving new input
        CurrentLoudness = Mathf.Max(0f, CurrentLoudness - loudnessDecaySpeed * Time.deltaTime);
    }

    public void OnAudioEvent(float decibels)
    {
        // Called by a PhotonVoice callback or footstep event
        CurrentLoudness = Mathf.Max(CurrentLoudness, decibels);
    }
}