using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //public SaveManager saveManger;
    public static AudioManager instance;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            AudioManager.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //saveManger = GameObject.FindObjectOfType<SaveManager>();
        //float music = PlayerPrefs.GetFloat("musicVolume", 0.0f);
        //float sfx = PlayerPrefs.GetFloat("sfxVolume", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
