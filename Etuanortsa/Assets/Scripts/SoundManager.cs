using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider VolumeSlider;

    [SerializeField]
    AudioSource Music ;
    public float MusicVolume = 0.25f;
    public void OnMovingSliderMusic(float Value)
    {
        MusicVolume = Value;
        Music.volume = Value;
    }
}
