using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global : MonoBehaviour
{
    public static global instance;

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

    public bool volumeBool;

    void Start()
    {
        volumeBool = false;
    }

    void Update()
    {
        if (GameManager.instance.juice)
        {
            volumeBool = true;
        }
        else
        {
            volumeBool = false;
        }
    }
}
