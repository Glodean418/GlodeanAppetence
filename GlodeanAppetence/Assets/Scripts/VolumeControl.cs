using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public static VolumeControl Instance;

    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer sfxMixer;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Start()
    {
        float savedMusicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSlider.value = savedMusicVol;
        UpdateMusicVolume();

        float savedSFXVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = savedSFXVol;
        UpdateSFXVolume();
    }
    public void UpdateMusicVolume()
    {
        musicMixer.SetFloat("musicVol", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume()
    {
        sfxMixer.SetFloat("sfxVol", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }
}
