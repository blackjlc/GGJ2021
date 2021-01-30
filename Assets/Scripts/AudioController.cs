using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioClip BottleCrash;
    public AudioClip PickUPItem;
    public AudioClip PutDownItem;
    public AudioClip FootStep;
    public AudioClip Dash;
    public AudioClip DoorIsLocked;
    public AudioClip DoorOpen;
    public AudioClip OpenLock;
    public AudioClip TapWater;
    public AudioClip ToiletFlush;
    public AudioClip GlassClinging;
    public AudioClip Ambience;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip SteppingOntheSlime;

    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    public void PlaySteppingSlime()
    {
        // sound.clip = SteppingOntheSlime;
        // sound.loop = true;
        sound.volume = .439f;
        sound.pitch = 1;
        sound.PlayOneShot(SteppingOntheSlime);
    }
    public void PlayBottleCrash()
    {
        // sound.clip = BottleCrash;
        // sound.loop = false;
        // sound.Play();
        sound.PlayOneShot(BottleCrash);
    }
    public void PlayPickUPItem()
    {
        sound.clip = PickUPItem;
        sound.loop = false;
        sound.Play();
    }
    public void PlayPutDownItem()
    {
        sound.clip = PutDownItem;
        sound.loop = false;
        sound.Play();
    }
    public void PlayFootStep()
    {
        sound.clip = FootStep;
        sound.loop = true;
        sound.Play();
    }
    public void PlayDash()
    {
        sound.clip = Dash;
        sound.loop = false;
        sound.volume = .125f;
        sound.pitch = .8f;
        sound.Play();
    }
    public void PlayDoorIsLocked()
    {
        sound.clip = DoorIsLocked;
        sound.loop = false;
        sound.Play();
    }
    public void PlayDoorOpen()
    {
        sound.clip = DoorOpen;
        sound.loop = false;
        sound.Play();
    }

    public void PlayOpenLock()
    {
        sound.clip = OpenLock;
        sound.loop = false;
        sound.Play();
    }

    public void PlayTapWater()
    {
        sound.clip = TapWater;
        sound.loop = false;
        sound.Play();
    }

    public void PlayToiletFlush()
    {
        sound.clip = ToiletFlush;
        sound.loop = false;
        sound.Play();
    }

    public void PlayGlassClinging()
    {
        // sound.clip = GlassClinging;
        // sound.loop = false;
        sound.PlayOneShot(GlassClinging);
    }

    public void PlayAmbience()
    {
        sound.clip = Ambience;
        sound.loop = true;
        sound.Play();
    }

    public void PlayMusic1()
    {
        sound.clip = music1;
        sound.loop = false;
        sound.Play();
    }

    public void PlayMusic2()
    {
        sound.clip = music2;
        sound.loop = false;
        sound.Play();
    }

    public void Stop()
    {
        sound.Stop();
    }
}
