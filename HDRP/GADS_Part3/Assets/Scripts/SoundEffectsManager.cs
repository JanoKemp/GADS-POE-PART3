using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton Pattern
    public static SoundManager Instance;

    // Audio Sources (one for music, multiple for effects)
    public AudioSource musicSource;
    public List<AudioSource> effectSources;
    public AudioSource eerieSoundSource; // Reference to the eerie sound AudioSource

    // Sound Library (using a Dictionary for organization)
    public Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();

    // Random Sound Settings
    public Vector2 randomIntervalRange = new Vector2(15f, 30f); 

    private void Awake()
    {
        // Singleton initialization
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize audio sources
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        effectSources = new List<AudioSource>();
        int numEffectSources = 3; 
        for (int i = 0; i < numEffectSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            effectSources.Add(source);
        }

        // Check if eerieSoundSource is assigned and has a clip
        if (eerieSoundSource == null || eerieSoundSource.clip == null)
        {
            Debug.LogError("EerieSoundSource is not assigned or has no audio clip attached.");
            return;
        }

        // Start coroutine to play the eerie sound at random intervals
        StartCoroutine(PlayEerieSoundLoop());
    }

    private IEnumerator PlayEerieSoundLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(randomIntervalRange.x, randomIntervalRange.y);
            yield return new WaitForSeconds(waitTime);

            eerieSoundSource.Play(); // Play the eerie sound
        }
    }

    public void PlayMusic(string clipName)
    {
        if (soundLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip '" + clipName + "' not found in sound library.");
        }
    }

    public void PlayEffect(string clipName)
    {
        if (soundLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            foreach (AudioSource source in effectSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.Play();
                    return;
                }
            }

            Debug.LogWarning("No free effect sources available.");
        }
        else
        {
            Debug.LogError("Audio clip '" + clipName + "' not found in sound library.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopAllEffects()
    {
        foreach (AudioSource source in effectSources)
        {
            source.Stop();
        }
    }
}
