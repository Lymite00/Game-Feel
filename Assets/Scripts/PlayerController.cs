using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public AudioSource aSource;
    public AudioClip shotSound;
    public AudioClip footstepSound;
    public AudioClip impactSound;
    public AudioClip impactEnemySound;

    private void Awake()
    {
        instance = this;
    }
    public void ShotSound()
    {
        aSource.PlayOneShot(shotSound);
    }
    public void PlayFootstepSound()
    {
        if (!aSource.isPlaying)
        {
            aSource.clip = footstepSound;
            aSource.Play();
        }
    }
    public void PlayImpactSound()
    {
        aSource.clip = impactSound;
        aSource.Play();
    }
    public void PlayEnemyImpactSound()
    {
        aSource.clip = impactEnemySound;
        aSource.Play();
    }
}
