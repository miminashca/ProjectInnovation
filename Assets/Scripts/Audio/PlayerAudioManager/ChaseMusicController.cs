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
        Debug.Log("thief player" + thiefPlayerObj);
        thiefPlayer = thiefPlayerObj;
        enemy = enemyObj;
        Debug.Log("thief player after set " + thiefPlayer);
//        if (thiefPlayer == null)
//            Debug.LogError("ChaseMusicController: thiefPlayer is NULL after SetReferences!");
//
//        if (enemy == null)
//            Debug.LogError("ChaseMusicController: enemy is NULL after SetReferences!");

//        Debug.Log("ChaseMusicController: SetReferences successfully set thiefPlayer and enemy.");
    }


    void Update()
    {
        Debug.Log(thiefPlayer);
      //  if (thiefPlayer == null || enemy == null)
      //      return;

        // Use a fixed distance for testing
        currentDistance = 10f;  // Set a fixed distance within activation range
        Debug.Log("Hello this is working");

        if (currentDistance <= activationDistance)
        {
            if (!isMusicPlaying)
            {
                StartMusic();  // This should definitely start the music if called
                isMusicPlaying = true;
                Debug.Log("Music is now playing.");
            }
            UpdateMusicLayers();
        }
    }


    void StartMusic()
    {
        Debug.Log("Starting music...");

        if (baseLayer != null)
        {
            Debug.Log("Base Layer: " + baseLayer.name);
            if (!baseLayer.isPlaying)
            {
                baseLayer.Play();
                Debug.Log("Base layer started playing.");
            }
        }
        else
        {
            Debug.LogError("Base Layer AudioSource is not assigned.");
        }

        if (intenseLayer != null)
        {
            Debug.Log("Intense Layer: " + intenseLayer.name);
            if (!intenseLayer.isPlaying)
            {
                intenseLayer.Play();
                Debug.Log("Intense layer started playing.");
            }
        }
        else
        {
            Debug.LogError("Intense Layer AudioSource is not assigned.");
        }

        if (crazyLayer != null)
        {
            Debug.Log("Crazy Layer: " + crazyLayer.name);
            if (!crazyLayer.isPlaying)
            {
                crazyLayer.Play();
                Debug.Log("Crazy layer started playing.");
            }
        }
        else
        {
            Debug.LogError("Crazy Layer AudioSource is not assigned.");
        }
    }




    void StopMusic()
    {
        Debug.Log("Stopping music...");
        baseLayer.Stop();
        intenseLayer.Stop();
        crazyLayer.Stop();
    }

    void UpdateMusicLayers()
    {
        float baseVolume = CalculateLayerVolume(baseStartDistance);
        float intenseVolume = CalculateLayerVolume(intenseStartDistance);
        float crazyVolume = CalculateLayerVolume(crazyStartDistance);

        Debug.Log($"Updated Volumes - Base: {baseVolume}, Intense: {intenseVolume}, Crazy: {crazyVolume}");

        baseLayer.volume = baseVolume;
        intenseLayer.volume = intenseVolume;
        crazyLayer.volume = crazyVolume;

        if (musicMixer != null)
        {
            musicMixer.SetFloat("BaseLayerVolume", Mathf.Lerp(-80f, 0f, baseVolume));
            musicMixer.SetFloat("IntenseLayerVolume", Mathf.Lerp(-80f, 0f, intenseVolume));
            musicMixer.SetFloat("CrazyLayerVolume", Mathf.Lerp(-80f, 0f, crazyVolume));
        }
    }

    float CalculateLayerVolume(float layerStartDistance)
    {
        if (currentDistance >= layerStartDistance) return 0f;
        if (currentDistance <= minDistance) return 1f;

        float fraction = (layerStartDistance - currentDistance) / (layerStartDistance - minDistance);
        float volume = Mathf.Clamp01(fraction);

        Debug.Log($"LayerStartDist: {layerStartDistance}, MinDist: {minDistance}, Fraction: {fraction}, Volume: {volume}");

        return volume;
    }
}
