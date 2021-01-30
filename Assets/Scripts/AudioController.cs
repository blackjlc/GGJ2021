using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioClip SteppingOntheSlime;

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void PlaySteppingSlime()
    {
        sound.clip = SteppingOntheSlime;
        sound.loop = true;
        sound.Play();
    }

    public void Stop()
    {
        sound.Stop();
    }
}
