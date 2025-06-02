using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundfxlibrary : MonoBehaviour
{
    [SerializeField] private SFXgroup[] sfxgroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SFXgroup sFXgroup in sfxgroups)
        {
            soundDictionary[sFXgroup.name] = sFXgroup.audioclips;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        if(soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if(audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }
        return null; 
    }

}

[System.Serializable]

public struct SFXgroup
{
    public string name;
    public List<AudioClip> audioclips;
}

