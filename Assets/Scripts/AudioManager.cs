
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public Sound[] music;
    public Sound[] effects;

    public AudioSource musicSrc, effectsSrc;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("ambience");
    }

    public void PlayMusic(string name)
    {
        Sound snd = Array.Find(music, x => x.name == name);

        if(snd == null)
        {
            Debug.Log("Sound not found");
        } else
        {
            musicSrc.clip = snd.clip;
            musicSrc.Play();
        }
    }

    public void PlayEffects(string name)
    {
        Sound snd = Array.Find(effects, x => x.name == name);

        if (snd == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            effectsSrc.PlayOneShot(snd.clip);
        }
    }
=======
    public Sound[] music;
    public Sound[] effects;

    public AudioSource musicSource, effectsSource;
}
