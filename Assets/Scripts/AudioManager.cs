using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundlist;

    public VendorManager vendor;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in soundlist)
        {
            //Audio files can be added to the list sound
            //they have a volume, pitch and loop ability
            //When we want to play a sound from the list at an event,
            //we can call the function
            //FindObjectOfType<AudioManager>().Play("NameofSound")
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    void Start()
    {
        Play("Ambient Sound");
    }
    
    public void Play(string name)
    {
        // searches the sound in the list and plays it
        Sound s = System.Array.Find(soundlist, sound => sound.name == name);
        s.source.Play();
        
        StartCoroutine(checkIfFinished(s.source));
    }

    IEnumerator checkIfFinished(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return null;
        }

        vendor.MakeReadyToSpeak();
    }
   
}