using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip brickHitSound, confirmSound, deathSound, 
    loadingSound, paddleHitSound, powerupSound,wallHitSound;
    static AudioSource audioSrc;

    void Start()
    {
        brickHitSound = Resources.Load<AudioClip> ("Sounds/brickHit");
        confirmSound = Resources.Load<AudioClip> ("Sounds/confirm");
        deathSound = Resources.Load<AudioClip> ("Sounds/death");
        loadingSound = Resources.Load<AudioClip> ("Sounds/loading");
        paddleHitSound = Resources.Load<AudioClip> ("Sounds/paddleHit");
        powerupSound = Resources.Load<AudioClip> ("Sounds/powerup");
        wallHitSound = Resources.Load<AudioClip> ("Sounds/wallHit");
    
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "brickHitSound":
                audioSrc.PlayOneShot(brickHitSound);
                break;
            case "confirmSound":
                audioSrc.PlayOneShot(confirmSound);
                break;
            case "deathSound":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "loadingSound":
                audioSrc.PlayOneShot(loadingSound);
                break;
            case "paddleHitSound":
                audioSrc.PlayOneShot(paddleHitSound);
                break;
            case "powerupSound":
                audioSrc.PlayOneShot(powerupSound);
                break;
            case "wallHitSound":
                audioSrc.PlayOneShot(wallHitSound);
                break;
        }
    }
}
