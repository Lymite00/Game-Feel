using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    
    public GameObject menuPanel;
    public GameObject controlsPanel;
    public GameObject levelPanel;

    public Sprite plus;
    public Sprite minus;
    public Image juiceImage;

    public Button juiceButton;

    public AudioSource aSource;
    public AudioClip clickSound;
    public AudioClip clickBackSound;
    public AudioClip onSound;

    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        levelPanel.SetActive(false);
        juiceButton.onClick.AddListener(Test);
    }

    public void ButtonClick()
    {
        if (GameManager.instance.juice)
        {
            aSource.clip = clickSound;
            aSource.Play();
        }
    }
    public void BackButtonClick()
    {
        if (GameManager.instance.juice)
        {
            aSource.clip = clickBackSound;
            aSource.Play();
        }
    }

    public void SwitchSound()
    {
        if (GameManager.instance.juice)
        {
            aSource.clip = onSound;
            aSource.Play();
        }
    }

    public void Test()
    {
        GameManager.instance.ToggleJuice();
    }
    void Update()
    {
        if (GameManager.instance.juice)
        {
            juiceImage.sprite = plus;
        }
        else
        {
            juiceImage.sprite = minus;
        }
    }

    public void BackMenu()
    {
        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        levelPanel.SetActive(false);
    }

    public void Play()
    {
        levelPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Itch()
    {
        Application.OpenURL("https://lymite.itch.io/");
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/lymitedev/");
    }

    public void Controls()
    {
        menuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2()
    {
        if (GameManager.instance.level2bool)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void Level3()
    {
        if (GameManager.instance.level3bool)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Level4()
    {
        if (GameManager.instance.level4bool)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void Level5()
    {
        if (GameManager.instance.level5bool)
        {
            SceneManager.LoadScene(5);
        }
    }
}
