using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shotInput;
    public static Action reloadInput;
    SpawnManager spawnManager;
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.gameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotInput?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadInput?.Invoke();
            }

        }
      
    }
}
