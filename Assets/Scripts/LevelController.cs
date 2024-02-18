using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public GameObject Finish1;
    public AudioSource aSource;
    public AudioClip ambientSound;

    
    
    public Transform[] spawnPoints; // Farklı spawn noktaları için bir dizi kullanın
    public GameObject enemyPrefab;

    public float spawnInterval = 2f;  // Düşman spawn aralığı
    public bool canSpawn = false;
    public int enemyCount;
    
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject gameUI;
    
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        gameUI.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        Finish1.SetActive(false);
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (enemyCount > 0)
        {
            if (canSpawn)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        healthText.text = PlayerHealthController.instance.currentHealth.ToString("D");
        
        if (GameManager.instance.juice)
        {
            AmbientSound();
        }
        
        if (enemyCount <= 0)
        {
            Finish1.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void AmbientSound()
    {
        if (!aSource.isPlaying)  // Ses çalmıyorsa
        {
            aSource.clip = ambientSound;
            aSource.Play();
        }
    }

    public void Win()
    {
        gameUI.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Lose()
    {
        gameUI.SetActive(false);
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void SpawnEnemy()
    {
        enemyCount -= 1;
        
        // Rastgele bir spawn noktası seçin
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void PlayAgain()
    {
        gameUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        gameUI.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameUI.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        gameUI.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
