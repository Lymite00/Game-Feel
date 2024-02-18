using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D theRB;
    public Vector2 moveDir;

    public GameObject bullet;
    private PlayerMovement player;

    public GameObject impactEffect;
    public GameObject enemyImpactEffect;
    public TrailRenderer tr;

    void Start()
    {
        tr = GetComponentInChildren<TrailRenderer>();
        tr.enabled = false;
        player = FindObjectOfType<PlayerMovement>();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        if (GameManager.instance.juice)
        {
            tr.enabled = true;
        }
        theRB.velocity = moveDir * bulletSpeed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (impactEffect != null && GameManager.instance.juice)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                PlayerController.instance.PlayEnemyImpactSound();
                PlayerController.instance.PlayImpactSound();
                Instantiate(enemyImpactEffect, transform.position, Quaternion.identity);
                
                EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    Vector2 direction = col.transform.position - transform.position;
                    direction.Normalize();
                    enemyController.PushEnemy(direction, bulletSpeed);
                }
            }
            else
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }
        }

        if (col.gameObject.CompareTag("Ground"))
        {
            if (GameManager.instance.juice)
            {
                PlayerController.instance.PlayImpactSound();
            }
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
        }
        Destroy(gameObject);
    }
}
