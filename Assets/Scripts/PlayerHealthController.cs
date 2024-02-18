using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth = 3;
    public float invincibilityDuration = 1f; // Hasar almamazlık süresi

    public int currentHealth;
    private bool isInvincible;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        isInvincible = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            currentHealth -= amount;
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(InvincibilityCoroutine());
            }
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        spriteRenderer.color = Color.gray; // Karakterin rengini koyulaştır
        yield return new WaitForSeconds(invincibilityDuration);
        spriteRenderer.color = originalColor; // Karakterin rengini eski haline getir
        isInvincible = false;
    }

    private void Die()
    {
        LevelController.instance.Lose();
        // Ölüm işlemleri
    }
}