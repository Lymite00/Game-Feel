using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;
    private bool isPushed = false; // İttirildi mi?

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Follow").transform;  // "Player" etiketine sahip karakterin transformunu bul
    }

    private void Update()
    {
        if (!isPushed)
        {
            // Karaktere doğru ilerleme
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void PushEnemy(Vector2 direction, float force)
    {
        isPushed = true;
        Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();
        if (enemyRB != null)
        {
            enemyRB.AddForce(direction * force, ForceMode2D.Impulse);
        }
        StartCoroutine(StopPushedEnemy());
    }

    private IEnumerator StopPushedEnemy()
    {
        yield return new WaitForSeconds(0.5f); // İstediğiniz süreyi ayarlayabilirsiniz
        isPushed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.TakeDamage(1);
        }
    }
}