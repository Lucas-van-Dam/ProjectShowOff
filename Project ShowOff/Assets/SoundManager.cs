using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct audioDictionaryBuilder
{
    public string key;
    public AudioClip audio;
}

public struct audioContainer
{
    public AudioClip clip;
    public AudioSource source;
    public audioContainer(AudioClip clip, AudioSource source)
    {
        this.clip = clip;
        this.source = source;
    }
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }

    [SerializeField]
    audioDictionaryBuilder[] sounds;

    private Dictionary<string, audioContainer> soundsMapped;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for(int i = 0; i < sounds.Length; i++)
        {
            AudioSource tempSource = this.AddComponent<AudioSource>();
            tempSource.clip = sounds[i].audio;

            soundsMapped.Add(sounds[i].key, new audioContainer(sounds[i].audio, tempSource));
        }

    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void playSound(string key)
    {
        soundsMapped[key].source.Play();
    }

    public void StopSound(string key)
    {
        soundsMapped[key].source.Stop();
    }

}
