using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSound : MonoBehaviour
{
    public AudioMixer Master;
    public Slider SliderMusic;
    public Slider SliderSound;

    private void Update()
    {
    
    }
    public void VolumeMusic()
    {
        float vol = SliderMusic.value;
        Master.SetFloat("Music", Mathf.Log10(vol) * 20);
    }
    public void VolumeSFX()
    {
        float vol = SliderSound.value;
        Master.SetFloat("SFX", Mathf.Log10(vol) * 20);
    }
}
