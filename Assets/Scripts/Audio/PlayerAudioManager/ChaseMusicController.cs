using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;

public class ChaseMusicController : MonoBehaviourPunCallbacks
{
    [Header("Optional Audio Mixer")]
    public AudioMixer musicMixer;

    [Header("Distances")]
    public float activationDistance = 40f;
    public float baseStartDistance = 30f;
    public float intenseStartDistance = 20f;
    public float crazyStartDistance = 10f;
    public float minDistance = 5f;

    [Header("Audio Sources")]
    public AudioSource baseLayer;
    public AudioSource intenseLayer;
    public AudioSource crazyLayer;

    private GameObject thiefPlayer;
    private GameObject enemy;

    private float currentDistance;
    private bool isMusicPlaying = false;

    void Start()
    {
        // Only the main player (thief) should control the chase music.
        if (!PhotonNetwork.IsMasterClient)
        {
            baseLayer.mute = true;
            intenseLayer.mute = true;
            crazyLayer.mute = true;
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Called from SpawnPlayers to assign the thief player and enemy.
    /// </summary>
    public void SetReferences(GameObject thiefPlayerObj, GameObject enemyObj)
    {
        //Debug.Log("thief player: " + thiefPlayerObj);
        thiefPlayer = thiefPlayerObj;
        enemy = enemyObj;
        //Debug.Log("thief player after set: " + thiefPlayer);
    }

    void Update()
    {
        // Ensure we have valid references.
        if (thiefPlayer == null || enemy == null)
            return;

        // Calculate the actual distance between the player and enemy.
        currentDistance = Vector3.Distance(thiefPlayer.transform.position, enemy.transform.position);
        //Debug.Log("Distance to enemy: " + currentDistance);

        if (currentDistance <= activationDistance)
        {
            // Start music if not already playing.
            if (!isMusicPlaying)
            {
                StartMusic();
                isMusicPlaying = true;
                //Debug.Log("Music is now playing.");
            }
            UpdateMusicLayers();
        }
        else if (isMusicPlaying)
        {
            // Optionally stop music if player is out of range.
            StopMusic();
            isMusicPlaying = false;
            //Debug.Log("Player is out of activation range. Music stopped.");
        }
    }

    void StartMusic()
    {
        //Debug.Log("Starting music...");

        if (baseLayer != null)
        {
            if (!baseLayer.isPlaying)
            {
                baseLayer.Play();
                //Debug.Log("Base layer started playing.");
            }
        }
        else
        {
            //Debug.LogError("Base Layer AudioSource is not assigned.");
        }

        if (intenseLayer != null)
        {
            if (!intenseLayer.isPlaying)
            {
                intenseLayer.Play();
                //Debug.Log("Intense layer started playing.");
            }
        }
        else
        {
            //Debug.LogError("Intense Layer AudioSource is not assigned.");
        }

        if (crazyLayer != null)
        {
            if (!crazyLayer.isPlaying)
            {
                crazyLayer.Play();
                //Debug.Log("Crazy layer started playing.");
            }
        }
        else
        {
            //Debug.LogError("Crazy Layer AudioSource is not assigned.");
        }
    }

    void StopMusic()
    {
        // Debug.Log("Stopping music...");
        if (baseLayer.isPlaying) baseLayer.Stop();
        if (intenseLayer.isPlaying) intenseLayer.Stop();
        if (crazyLayer.isPlaying) crazyLayer.Stop();
    }

    void UpdateMusicLayers()
    {
        // Calculate volumes for each layer based on current distance.
        float baseVolume = CalculateLayerVolume(baseStartDistance);
        float intenseVolume = CalculateLayerVolume(intenseStartDistance);
        float crazyVolume = CalculateLayerVolume(crazyStartDistance);

        // Debug.Log($"Updated Volumes - Base: {baseVolume}, Intense: {intenseVolume}, Crazy: {crazyVolume}");

        baseLayer.volume = baseVolume;
        intenseLayer.volume = intenseVolume;
        crazyLayer.volume = crazyVolume;

        // Update AudioMixer parameters if one is used.
        if (musicMixer != null)
        {
            musicMixer.SetFloat("BaseLayerVolume", Mathf.Lerp(-80f, 0f, baseVolume));
            musicMixer.SetFloat("IntenseLayerVolume", Mathf.Lerp(-80f, 0f, intenseVolume));
            musicMixer.SetFloat("CrazyLayerVolume", Mathf.Lerp(-80f, 0f, crazyVolume));
        }
    }

    float CalculateLayerVolume(float layerStartDistance)
    {
        // If the player is farther than the start distance for this layer, the volume is zero.
        if (currentDistance >= layerStartDistance) return 0f;
        // If the player is very close (within minDistance), volume is maximum.
        if (currentDistance <= minDistance) return 1f;

        // Otherwise, calculate a normalized fraction where volume increases as distance decreases.
        float fraction = (layerStartDistance - currentDistance) / (layerStartDistance - minDistance);
        float volume = Mathf.Clamp01(fraction);

        // Debug.Log($"LayerStartDist: {layerStartDistance}, MinDist: {minDistance}, Fraction: {fraction}, Volume: {volume}");

        return volume;
    }
}