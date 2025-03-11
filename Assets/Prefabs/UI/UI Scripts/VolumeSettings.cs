using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicValue"))
        {
            LoadVolume();
        }
        else
        {
            setMusicVolume();
        }
       
    }
    public void setMusicVolume()
    {
        float musicVolume = musicSlider.value;
        MusicMixer.SetFloat("MasterVolume", Mathf.Log10(musicVolume)*20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume); //store the slider info inetween scenes and whatnot
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume"); //get the stored val
        setMusicVolume();
    }
}
