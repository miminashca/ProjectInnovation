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
   
}
