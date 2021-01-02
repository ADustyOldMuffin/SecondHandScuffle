using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSFXVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volSlider;
    float currentVolSliderValue;
    AudioSource testAudio;
    private bool firstTime = true;
    [SerializeField] float defaultSFXVolume = .5f;

    void Start()
    {
        testAudio = GetComponent<AudioSource>();
        if (volSlider != null)
        {
            volSlider.value = PlayerPrefs.GetFloat("SFXVol", defaultSFXVolume);
            currentVolSliderValue = volSlider.value;
        }
        //testAudio = GetComponent<AudioSource>();
        //Debug.Log("start slider value " + currentVolSliderValue.ToString());

        //Debug.Log("mute at start " + PlayerPrefs.GetInt("Muted", 0).ToString());
    }
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
        currentVolSliderValue = sliderValue;
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            testAudio.Play();
        }
    }

    public void UnmuteSFX()
    {
        volSlider.value = PlayerPrefs.GetFloat("SFXVolumeBeforeMuting");
        mixer.SetFloat("SFXVol", Mathf.Log10(volSlider.value) * 20);
        //PlayerPrefs.SetInt("Muted", 0);

        //Debug.Log("unmute slider value " + currentVolSliderValue.ToString());
    }
    public void MuteSFX()
    {
        PlayerPrefs.SetFloat("SFXVolumeBeforeMuting", currentVolSliderValue);
        mixer.SetFloat("SFXVol", 0.0001f);
        volSlider.value = 0.0001f;
        //PlayerPrefs.SetInt("Muted", 1);

        //Debug.Log("mute slider value " + currentVolSliderValue.ToString());
    }

    public void SetSFXToDefault()
    {
        volSlider.value = defaultSFXVolume;
        mixer.SetFloat("SFXVol", Mathf.Log10(volSlider.value) * 20);
        PlayerPrefs.SetInt("Muted", 0);
    }
}
