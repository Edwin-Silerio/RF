﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A singleton class that provides singleton support for anything that needs to be a singleton.
 * All singletons can be referenced from afar here.
 */
public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance => instance;

    [SerializeField] private GameManager gameManager = default;
    public static GameManager GameManager => instance?.gameManager;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }
}
