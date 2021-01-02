using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volSlider;
    float currentVolSliderValue;
    [SerializeField] float defaultMusicLevel = 0.5f;

    void Start()
    {
        if (volSlider != null)
        {
            volSlider.value = PlayerPrefs.GetFloat("MusicVol", defaultMusicLevel);
            currentVolSliderValue = volSlider.value;
        }
        //Debug.Log("start slider value " + currentVolSliderValue.ToString());

        //Debug.Log("mute at start " + PlayerPrefs.GetInt("Muted", 0).ToString());
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
        currentVolSliderValue = sliderValue;
    }

    void Update()
    {
        //no longer works because of new input system. fix if time
        /**
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (PlayerPrefs.GetInt("Muted") == 1)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
        }
        **/
    }

    private void Unmute()
    {
        volSlider.value = PlayerPrefs.GetFloat("MusicVolumeBeforeMuting");
        mixer.SetFloat("MusicVol", Mathf.Log10(volSlider.value) * 20);
        PlayerPrefs.SetInt("Muted", 0);

        FindObjectOfType<SetSFXVolume>().UnmuteSFX();

        //Debug.Log("unmute slider value " + currentVolSliderValue.ToString());
    }
    private void Mute()
    {
        PlayerPrefs.SetFloat("MusicVolumeBeforeMuting", currentVolSliderValue);
        mixer.SetFloat("MusicVol", 0.0001f);
        volSlider.value = 0.0001f;
        PlayerPrefs.SetInt("Muted", 1);



        FindObjectOfType<SetSFXVolume>().MuteSFX();
        //Debug.Log("mute slider value " + currentVolSliderValue.ToString());
    }

    public void DefaultSettings()
    {
        volSlider.value = defaultMusicLevel;
        mixer.SetFloat("MusicVol", Mathf.Log10(volSlider.value) * 20);
        PlayerPrefs.SetInt("Muted", 0);
        FindObjectOfType<SetSFXVolume>().SetSFXToDefault();
    }
}
