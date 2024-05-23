using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
public class testSeting : MonoBehaviour
{
    public Slider MusicVol, sfxVol;
    public AudioMixer mainAudioMixer;

    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    public void ChangeGraphicsQualyfi()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Musicvol", MusicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXvol",sfxVol.value);
    }
}
