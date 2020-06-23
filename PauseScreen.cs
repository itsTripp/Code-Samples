using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public SettingsMenu settingsMenu;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public Toggle musicTogglePause;
    public Toggle sfxTogglePause;

    public float musicIsOn = 0.0f;
    public float musicIsOff = -80f;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>();   
    }

    // Update is called once per frame
    void Update()
    {
        //settingsMenu.CheckMusicOn();
        //settingsMenu.CheckSFXOn();
        IsMusicOn();
    }

    public void IsMusicOn()
    {
        if (musicMixer.GetFloat("musicVolume", out musicIsOn))
        {
            settingsMenu.musicOnImage.SetActive(true);
            musicTogglePause.isOn = true;
            settingsMenu.musicOffImage.SetActive(false);
            settingsMenu.SetMusicVolume(true);
        }
        if(musicMixer.GetFloat("musicVolume", out musicIsOff))
        {
            settingsMenu.musicOffImage.SetActive(true);
            musicTogglePause.isOn = false;
            settingsMenu.musicOnImage.SetActive(false);
            settingsMenu.SetMusicVolume(false);
        }
    }

    
}
