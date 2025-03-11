using UnityEngine;

public class AudioSensor : MonoBehaviour
{
    // audio sensor reads players audio input, and checks if it passes the threshold for enemy to hear the player.
    // if that is true - it passes this information to AI director, which in its turn passes this event to state machine.
    
    // This script can accumulate loudness from Photon Voice or footstep events
    [SerializeField] private float loudnessDecaySpeed = 1f;

    public float CurrentLoudness { get; private set; }

    private void Update()
    {
        CurrentLoudness = Mathf.Max(0f, CurrentLoudness - loudnessDecaySpeed * Time.deltaTime);
    }

    public void OnAudioEvent(float decibels)
    {
        // Called by a PhotonVoice callback or footstep event
        CurrentLoudness = Mathf.Max(CurrentLoudness, decibels);
    }
}