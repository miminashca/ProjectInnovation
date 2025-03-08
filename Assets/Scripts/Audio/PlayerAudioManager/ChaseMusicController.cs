using UnityEngine;
using UnityEngine.Audio;

public class ChaseMusicController : MonoBehaviour
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

    private GameObject player;
    private GameObject enemy;
    private float currentDistance;
    private bool isMusicPlaying = false;

    public void SetTargets(GameObject playerObj, GameObject enemyObj)
    {
        player = playerObj;
        enemy = enemyObj;
    }

    void Update()
    {
        if (player == null || enemy == null) return;

        currentDistance = Vector3.Distance(player.transform.position, enemy.transform.position);

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
        float baseVolume = CalculateLayerVolume(baseStartDistance);
        float intenseVolume = CalculateLayerVolume(intenseStartDistance);
        float crazyVolume = CalculateLayerVolume(crazyStartDistance);

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
        if (currentDistance > layerStartDistance)
            return 0f;

        float normalizedValue = Mathf.InverseLerp(layerStartDistance, minDistance, currentDistance);
        return Mathf.Lerp(0f, 1f, normalizedValue);
    }
}
