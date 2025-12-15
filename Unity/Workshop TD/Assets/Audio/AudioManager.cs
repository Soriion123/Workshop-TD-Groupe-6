using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;
    }

    public List<Sound> sounds = new List<Sound>();
    private AudioSource source;
    private AudioSource loopSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        source = gameObject.AddComponent<AudioSource>();
        loopSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        Sound s = sounds.Find(s => s.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return;
        }

        source.pitch = s.pitch;
        source.PlayOneShot(s.clip, s.volume);
    }

    public void PlayLoop(string soundName)
    {
        Sound s = sounds.Find(s => s.name == soundName);
        if (s == null) return;

        loopSource.clip = s.clip;
        loopSource.volume = s.volume;
        loopSource.pitch = s.pitch;
        loopSource.loop = true;

        if (!loopSource.isPlaying)
            loopSource.Play();
    }
    public void StopLoop()
    {
        loopSource.Stop();
    }

}
