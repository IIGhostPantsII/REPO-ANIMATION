using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.GetComponent<AudioSource>();
            if(audioSource == null)
            {
                Debug.LogError("No AudioSource found on the SoundManager GameObject.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if(clip != null)
        {
            string clipName = clip.name;
            
            GameObject audioObject = new GameObject("Sound_" + clipName);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            CopyAudioSourceSettings(gameObject.GetComponent<AudioSource>(), audioSource);

            audioSource.clip = clip;
            audioSource.Play();

            Destroy(audioObject, clip.length);
        }
    }

    public void PlaySound(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + clipName);

        if(clip != null)
        {
            GameObject audioObject = new GameObject("Sound_" + clipName);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            CopyAudioSourceSettings(gameObject.GetComponent<AudioSource>(), audioSource);

            audioSource.clip = clip;
            audioSource.Play();

            Destroy(audioObject, clip.length);
        }
        else
        {
            Debug.LogError("AudioClip not found: " + clipName + ". Please ensure the audio clip exists in Resources/Sound and the name is correct.");
        }
    }

    public void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in allAudioSources)
        {
            source.Stop();
        }
    }

    private void CopyAudioSourceSettings(AudioSource source, AudioSource target)
    {
        target.volume = source.volume;
        target.pitch = source.pitch;
        target.spatialBlend = source.spatialBlend;
        target.loop = source.loop;
        target.mute = source.mute;
        target.outputAudioMixerGroup = source.outputAudioMixerGroup;
        target.priority = source.priority;
        target.panStereo = source.panStereo;
        target.reverbZoneMix = source.reverbZoneMix;
        target.dopplerLevel = source.dopplerLevel;
        target.spread = source.spread;
        target.rolloffMode = source.rolloffMode;
        target.minDistance = source.minDistance;
        target.maxDistance = source.maxDistance;
    }
}
