using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth instance;
    
    public int maxHealth;
    private int currentHealth;
    
    private bool isDead = false;
    
    public AudioSource aSource;
    public AudioClip dieSound;
    public AudioClip hurtSound;

    public SpriteRenderer sr;
    public BoxCollider2D cc;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;;
    }
    public void TakeDamage(int damageAmount)
    {
        if (isDead) // Eğer düşman zaten ölüyse, hiçbir şey yapma
            return;

        currentHealth -= damageAmount;

        if (GameManager.instance.juice && !aSource.isPlaying)
        {
            aSource.clip = hurtSound;
            aSource.Play();
        }

        if (currentHealth <= 0)
        {
            if (GameManager.instance.juice)
            {
                aSource.clip = dieSound;
                aSource.Play();
            }

            isDead = true; // Düşman öldü

            DestroyEnemy();
        }
    }
    public void DestroyEnemy()
    {
        sr.enabled = false;
        cc.enabled = false;
        Destroy(gameObject,0.8f);
    }
    public void DestroyInstant()
    {
        Destroy(gameObject);
    }
}