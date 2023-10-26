using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    
    // Start is called before the first frame update

    Target[] target;
    SpawnerTarget[] spawnTarget;
    int count = 0;

    public bool gameStart = false;
    float timer = 0;
    public float timerDuration = 5.0f;

    //public GameObject spotLight;

    public GameObject Spawner1;
    public GameObject Spawner2;

    public GameObject gamesStart;

    int counting = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        timer = timerDuration;
        spawnTarget = FindObjectsOfType<SpawnerTarget>();
    }
    public void StartGame()
    {
        gamesStart.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameStart = true;
       
    }

    void Update()
    {

        target = FindObjectsOfType<Target>();

        count = 0; 

        if (target.Length > 0)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i].alive)
                {
                    count++;
                    //gameStart= true;
                }
            }
        }


        counting = 0;

        foreach (Target target1 in target)
        {
            if (target1.alive)
            {
                counting++;
            }

        }
        if (counting <= 0)
        {
            Spawner1.SetActive(false);
            Spawner2.SetActive(false);
        }
        int selectedCount = 0;
        foreach (Target target1 in target)
        {
            if (target1.isSelected)
            {
                selectedCount++;
            }
        }
        Debug.Log(count);

     
        if (count <= 0)
        {
            if (timerDuration > 0)
            {
                timerDuration -= Time.deltaTime;
            }
            else
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    Spawner1.SetActive(true);
                    Spawner2.SetActive(false);
                }
                else
                {
                    Spawner1.SetActive(false);
                    Spawner2.SetActive(true);
                }

                timerDuration = Random.Range(1, 4);
                //gameStart = false;

                foreach (SpawnerTarget sT in spawnTarget)
                {
                   
                    sT.targetsAliveCopy.Clear();
                    sT.GiveHealth();
                }
            }
        }
    }

}
