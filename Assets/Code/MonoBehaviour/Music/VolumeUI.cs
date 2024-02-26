using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeUI : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private AudioMixerGroup masterGroup;
    [SerializeField]
    private AudioMixerGroup musicGroup;
    [SerializeField]
    private AudioMixerGroup sfxGroup;

    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    void Start ()
    {
        // do we have saved volume player prefs?
        if(PlayerPrefs.HasKey("Master"))
        {
            // set the mixer volume levels based on the saved player prefsñ
            mixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
            mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
            mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));

            SetSliders();
        }
        // otherwise just set the sliders
        else
        {
            SetSliders();
        }
    }

    // called at the start of the game
    // set the slider values to be the saved volume settings
    void SetSliders ()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        musicSlider.value = PlayerPrefs.GetFloat("Music");
    }

    // called when we update the master slider
    public void UpdateMasterVolume ()
    {
        mixer.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("Master", masterSlider.value);
    }

    // called when we update the sfx slider
    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
    }

    // called when we update the music slider
    public void UpdateMusicVolume()
    {
        mixer.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
    }
}