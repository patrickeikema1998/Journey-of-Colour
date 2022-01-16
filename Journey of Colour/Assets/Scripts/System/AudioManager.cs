using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sounds;

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


    }

    private void Start()
    {
        PlayOrStop("birds", true);
    }

    public void PlayOrStop(string name, bool play)
    {
        AudioSource s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        if (play && !s.isPlaying) s.Play();
        else if (!play) s.Stop();
    }

   public void PlayOrStop(string name, bool play, Vector2 randomPitch)
    {
        AudioSource s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.pitch = UnityEngine.Random.Range(randomPitch.x, randomPitch.y);

        if (play && !s.isPlaying) s.Play();
        else if (!play) s.Stop();
    }

    public void Play(string name)
    { 
        AudioSource s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.Play();
    }

}
