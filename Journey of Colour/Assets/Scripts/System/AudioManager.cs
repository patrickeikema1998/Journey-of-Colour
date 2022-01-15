using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

            DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = 1f;
        }
    }

    private void Start()
    {
        PlayOrStop("birds", true);
    }

    public void PlayOrStop(string name, bool play)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        if (play && !s.source.isPlaying) s.source.Play();
        else if (!play) s.source.Stop();
    }

   public void PlayOrStop(string name, bool play, Vector2 randomPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.pitch = UnityEngine.Random.Range(randomPitch.x, randomPitch.y);

        if (play && !s.source.isPlaying) s.source.Play();
        else if (!play) s.source.Stop();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.Play();
    }

}
