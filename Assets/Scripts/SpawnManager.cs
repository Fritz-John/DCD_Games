using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpawnManager : MonoBehaviour
{
    
    // Start is called before the first frame update

    Target[] target;
    SpawnerTarget[] spawnTarget;
    int count = 0;

    bool gameStart = false;
    float timer = 0;
    public float timerDuration = 5.0f;

    public GameObject spotLight;
    void Start()
    {
        spotLight.SetActive(true);
        timer = timerDuration;
        spawnTarget = FindObjectsOfType<SpawnerTarget>();
    }

    // Update is called once per frame
    void Update()
    {

        target = FindObjectsOfType<Target>();

        count = 0;  // Reset the count in each frame
        if (target.Length > 0)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i].alive)
                {
                    count++;
                    gameStart= true;
                }
            }
        }

        //Debug.Log("Number of alive targets: " + count);

        if(count <= 0 && gameStart)
        {
            if(timerDuration > 0)
            {
                timerDuration -= Time.deltaTime;
                spotLight.SetActive(false);
            }
            else
            {
                spotLight.SetActive(true);
                int rand = Random.Range(1, 3);
                timerDuration = rand;
                gameStart = false;
                foreach (SpawnerTarget sT in spawnTarget)
                {
                    sT.targetsAliveCopy.Clear();
                    sT.GiveHealth();
                   
                }
               
            }
           
        }
    }

}
