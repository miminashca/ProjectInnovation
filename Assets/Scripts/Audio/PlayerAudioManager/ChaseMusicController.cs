using UnityEngine;
using UnityEngine.Audio;

public class ChaseMusicController : MonoBehaviour
{
    public GameObject player;                   // Reference to the thief player
    public GameObject enemy;                    // Reference to the roaming monster
    public AudioSource baseLayer;               // Base chase music (low intensity)
    public AudioSource intenseLayer;            // More intense chase music
    public AudioSource crazyLayer;              // Most intense chase music
    public AudioMixer musicMixer;               // Reference to the AudioMixer (optional)

    public float activationDistance = 40f;      // Distance at which music starts playing
    public float baseStartDistance = 30f;       // Distance where the base layer starts fading in
    public float intenseStartDistance = 20f;    // Distance where the intense layer starts fading in
    public float crazyStartDistance = 10f;      // Distance where the crazy layer starts fading in
    public float minDistance = 5f;              // Min distance (full intensity)

    private float currentDistance;              // Current distance between thief and monster
    private bool isMusicPlaying = false;        // Track if music is currently playing

    void Update()
    {
        if (player == null || enemy == null) return;

        // Get distance between thief and monster
        currentDistance = Vector3.Distance(player.transform.position, enemy.transform.position);

        // If the monster is within range, start the music
        if (currentDistance <= activationDistance)
        {
            if (!isMusicPlaying)
            {
                StartMusic();
                isMusicPlaying = true;
            }

            UpdateMusicLayers();
        }
        else
        {
            if (isMusicPlaying)
            {
                StopMusic();
                isMusicPlaying = false;
            }
        }
    }

    void StartMusic()
    {
        baseLayer.Play();
        intenseLayer.Play();
        crazyLayer.Play();
    }

    void StopMusic()
    {
        baseLayer.Stop();
        intenseLayer.Stop();
        crazyLayer.Stop();
    }

    void UpdateMusicLayers()
    {
        // Compute fade-in values per layer
        float baseVolume = CalculateLayerVolume(baseStartDistance);
        float intenseVolume = CalculateLayerVolume(intenseStartDistance);
        float crazyVolume = CalculateLayerVolume(crazyStartDistance);

        // Apply volume
        baseLayer.volume = baseVolume;
        intenseLayer.volume = intenseVolume;
        crazyLayer.volume = crazyVolume;

        // If using an AudioMixer, update exposed parameters
        if (musicMixer != null)
        {
            musicMixer.SetFloat("BaseLayerVolume", Mathf.Lerp(-80f, 0f, baseVolume));
            musicMixer.SetFloat("IntenseLayerVolume", Mathf.Lerp(-80f, 0f, intenseVolume));
            musicMixer.SetFloat("CrazyLayerVolume", Mathf.Lerp(-80f, 0f, crazyVolume));
        }
    }

    float CalculateLayerVolume(float layerStartDistance)
    {
        if (currentDistance > layerStartDistance)
            return 0f; // Fully silent if the monster is too far

        float normalizedValue = Mathf.InverseLerp(layerStartDistance, minDistance, currentDistance);
        return Mathf.Lerp(0f, 1f, normalizedValue); // Fade-in effect
    }
}
