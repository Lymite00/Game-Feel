using System;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public static BombController instance;
    public AudioSource aSource;
    public AudioClip explosionSound;

    public GameObject explosionEffect;
    public float explosionRadius;
    public float explosionForce;

    public CircleCollider2D cr2d;

    public bool canDestroyEnemies = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cr2d = GetComponentInChildren<CircleCollider2D>();
        cr2d.enabled = false;
        Invoke("Explosion",2f);
        Invoke("Active",1.9f);
        Destroy(gameObject,3f);
    }

    void Update()
    {
        
    }

    public void Active()
    {
        cr2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (canDestroyEnemies)
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.DestroyInstant();
                }
            }
        }
    }


    public void Explosion()
    {
        ExplosionSound();
        if (GameManager.instance.juice)
        {
            CameraShaker.Instance.ShakeOnce(12f, 2f, 0f, 0.5f);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            TrailRenderer tr = GetComponentInChildren<TrailRenderer>();
            tr.enabled = false;
            sr.enabled = false;
            cr2d.enabled = false;

            canDestroyEnemies = true;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    if (canDestroyEnemies)
                    {
                        EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.DestroyInstant();
                        }
                    }
                }
            }
        }
    }

    public void ExplosionSound()
    {
        if (GameManager.instance.juice)
        {
            aSource.clip = explosionSound;
            aSource.Play();
        }
    }
}
