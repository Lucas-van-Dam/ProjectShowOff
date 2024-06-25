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
    public bool looping;
    [Range(0f, 1f)] public float volume;
}

public struct audioContainer
{
    public AudioClip clip;
    public bool looping;
    public AudioSource source;
    public float volume;
    public audioContainer(AudioClip clip, bool looping, AudioSource source, float volume)
    {
        this.clip = clip;
        this.looping = looping;
        this.source = source;
        this.volume = volume;
    }
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }

    [SerializeField]
    audioDictionaryBuilder[] sounds;

    private Dictionary<string, audioContainer> soundsMapped;

    [SerializeField] AudioSource audioSource;

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

        soundsMapped = new Dictionary<string, audioContainer>();

        for(int i = 0; i < sounds.Length; i++)
        {
            if (!sounds[i].looping)
            {
                soundsMapped.Add(sounds[i].key, new audioContainer(sounds[i].audio, false, audioSource, sounds[i].volume));
            }
            else
            {
                AudioSource tempSource = gameObject.AddComponent<AudioSource>();
                tempSource.clip = sounds[i].audio;
                tempSource.loop = true;
                tempSource.volume = sounds[i].volume;

                soundsMapped.Add(sounds[i].key, new audioContainer(sounds[i].audio, true, tempSource, sounds[i].volume));
            }
        }

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        PlayLoop("musicloop");
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void PlaySound(string key)
    {

        audioSource.PlayOneShot(soundsMapped[key].clip, soundsMapped[key].volume);
    }

    public void PlayLoop(string key)
    {
        soundsMapped[key].source.Play();
    }

    public void StopLoop(string key)
    {
        soundsMapped[key].source.Stop();
        //audioSource.PlayOneShot(soundsMapped[key].clip);
    }

}
