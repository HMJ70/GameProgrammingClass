using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfxmanager : MonoBehaviour
{
    private static sfxmanager instance;

    private static AudioSource audioSource;
    private static soundfxlibrary sfxlibrary;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            sfxlibrary =  GetComponent<soundfxlibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = sfxlibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    

    public static void setvolume(float volume)
    {
        audioSource.volume = volume;
    }

   


}
