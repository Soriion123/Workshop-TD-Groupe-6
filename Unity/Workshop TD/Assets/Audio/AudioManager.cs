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

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        source = gameObject.AddComponent<AudioSource>();
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
}
