using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip clickSound, walkSound,waterjump,mouthsound,onBarkSound,wrongclick,winSound,loseSound,pickupSound;
    public AudioSource As;
    public AudioSource WalkAs;
    public AudioSource MouthAs;

    public void onBtClickSound()
    {
        As.PlayOneShot(clickSound);
    }
    public void onBtwrongClickSound()
    {
        As.PlayOneShot(wrongclick);
    }
    public void musicBt()
    {
        if (!this.GetComponent<AudioSource>().mute)
        {
            this.GetComponent<AudioSource>().mute = true;
          //  As.mute = true;
            WalkAs.mute = true;
            MouthAs.mute = true;
        }
        else
        {
            this.GetComponent<AudioSource>().mute = false;
           // As.mute = false;
            WalkAs.mute = false;
            MouthAs.mute = false;
        }
    }

    public void onwalk()
    {
        if (!WalkAs.isPlaying)
        {
            WalkAs.pitch = 1;
            WalkAs.clip = walkSound;
            WalkAs.Play();
            MouthAs.clip = mouthsound;
            MouthAs.Play();
        }
    }
    public void onbarkSound()
    {
        As.PlayOneShot(onBarkSound);
    }
    public void onWinSound()
    {
        As.PlayOneShot(winSound);
    }
    public void onPickupSound()
    {
        As.PlayOneShot(pickupSound);
    }
    public void onLoseSound()
    {
        As.PlayOneShot(loseSound);
    }
    public void onwalkstop()
    {
        if (WalkAs.isPlaying)
        {
            WalkAs.pitch = 1;
            WalkAs.Stop();
           
            MouthAs.Stop();
        }
    }
    public void onrun()
    {
        if (!WalkAs.isPlaying)
        {
            WalkAs.pitch = 2;
            WalkAs.clip = walkSound;
            WalkAs.Play();
        }
    }
    public void onrunstop()
    {
        if (WalkAs.isPlaying)
        {
            WalkAs.pitch = 2;
            WalkAs.Stop();
        }
    }

    public void onwater()
    {
        
                
            As.PlayOneShot(waterjump);
        
    }
}
