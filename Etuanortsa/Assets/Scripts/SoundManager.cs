using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider VolumeSlider;
    float MusicVolume;

    [SerializeField]
    AudioSource Music ;
    

    private void Start()
    {
        MusicVolume = VolumeSlider.value;
    }
    public void OnMovingSliderMusic(float Value)
    {
        MusicVolume = Value;
        Music.volume = Value;
    }
}
