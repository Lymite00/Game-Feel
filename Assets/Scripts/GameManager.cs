using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool juice;

    public bool level1bool;
    public bool level2bool;
    public bool level3bool;
    public bool level4bool;
    public bool level5bool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        level1bool = true;
        level2bool = false;
        level3bool = false;
        level4bool = false;
        level5bool = false;

        juice = false;
    }

    private void Update()
    {
        
    }

    public void ToggleJuice()
    {
        juice = !juice;
    }
}