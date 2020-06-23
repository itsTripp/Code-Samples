using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioManager audioManager;

    //public Toggle audioToggle;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    //public AudioMixer audioMixer;
    //public AudioMixer musicMixer;
    //public AudioMixer sfxMixer;

    public GameObject musicOnImage;
    public GameObject musicOffImage;
    public GameObject sfxOnImage;
    public GameObject sfxOffImage;

    private float musicVolume;
    private float sfxVolume;

    private float musicIsOn = 0.0f;
    private float musicIsOff = -80f;

    //Toggles for Audio

    //Master Audio(Not Currently Used)
    /*public void SetVolume(bool volume)
    {
        if(audioToggle.isOn)
        {
            audioMixer.SetFloat("volume", 0.0f);
        }
        else
        {
            audioMixer.SetFloat("volume", -80f);
        }
    }
    */

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        CheckMusicOn();
        CheckSFXOn();
    }

    private void Update()
    {
        CheckMusicOn();
        CheckSFXOn();
    }

    //Music Volume
    public void SetMusicVolume(bool volume)
    {
        if (musicToggle.isOn)
        {
            audioManager.musicMixer.SetFloat("musicVolume", 0.0f);
            musicVolume = 0.0f;
        }
        else if(!musicToggle.isOn)
        {
            audioManager.musicMixer.SetFloat("musicVolume", -80f);
            musicVolume = -80f;
        }
    }

    //SFX Volume
    public void SetSFXVolume(bool volume)
    {
        if (sfxToggle.isOn)
        {
            audioManager.sfxMixer.SetFloat("sfxVolume", 0.0f);
            sfxVolume = 0.0f;
        }
        else if(!sfxToggle.isOn)
        {
            audioManager.sfxMixer.SetFloat("sfxVolume", -80f);
            sfxVolume = -80f;
        }
    }

    public void CheckMusicOn()
    {
        if (musicVolume == 0.0f)
        {
            musicToggle.isOn = true;
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);
        }
        if (musicVolume == -80f)
        {
            musicToggle.isOn = false;
            musicOffImage.SetActive(true);
            musicOnImage.SetActive(false);
        }
    }

    public void CheckSFXOn()
    {
        if(sfxVolume == 0.0f)
        {
            sfxToggle.isOn = true;
            sfxOnImage.SetActive(true);
            sfxOffImage.SetActive(false);
        }
        if(sfxVolume == -80f)
        {
            sfxToggle.isOn = false;
            sfxOffImage.SetActive(true);
            sfxOnImage.SetActive(false);
        }
    }

    //These are used for sliders if they are to be implemented back in.

    /*public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("sfxVolume", volume);
    }
    */
}
