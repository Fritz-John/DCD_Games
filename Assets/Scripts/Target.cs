using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour, IDamageable
{
    public Text currentTimeAlive;
    public float health;
   
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public GameObject targetToRotate;

    public float speed;

    float timeCount = 0.0f;

    public bool alive = false;

    public float timeLeft;
    public int minTime;
    public int maxTime;
    private float setTimer;
    SpawnerTarget target;

    private int targetID;
    private void Awake()
    {
        
        targetID = GetInstanceID();


    }

    private void Start()
    {
        health = 0;
        setTimer = timeLeft;
 
    }
    public void Damage(float damage)
    {
        timeCount = 0.0f;

        health -= damage;
        setSpeed();
        if (target != null)
        {
            target.RemoveTargetAtList(gameObject);
        }
       
    }

    private void Update()
    {
        //target = FindObjectOfType<SpawnerTarget>();
        currentTimeAlive.text = timeLeft.ToString();
        if (health <= 0)
        {
            initialRotation = Quaternion.Euler(-90f, 0f, -90f);
            targetRotation = Quaternion.Euler(0f, 0f, -90f);
            alive = false;

        }
        else if (health > 0)
        {
            initialRotation = Quaternion.Euler(0f, 0f, -90f);
            targetRotation = Quaternion.Euler(-90f, 0f, -90f);
            alive = true;
        }

        timeCount += Time.deltaTime;
     
            targetToRotate.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, timeCount * speed);
        

        if (alive)
        {
            if (timeLeft > 0)
            {

                timeLeft -= Time.deltaTime;

            }
            else if(timeLeft <= 0)
            {

                alive = false;
                timeCount = 0.0f;
                setSpeed();
                health = 0;
                PlayerMovement player = FindObjectOfType<PlayerMovement>();
                player.MinusHealth();
               
                //SpawnerTarget.instance.RemoveTargetAtList(gameObject);

            }
        }
        //else
        //{
        //    SpawnerTarget spawnerTarget = FindObjectOfType<SpawnerTarget>();
        //    if (spawnerTarget != null)
        //    {
        //        spawnerTarget.RemoveTargetAtList(gameObject);
        //    }

        //}
    }

    public void setHealth(float num)
    {

        if (health != num)
        {
            timeCount = 0.0f;
        }
        health = num;
    }
    public void setSpeed()
    {
        float randSpeed = Random.Range(10f, 12f);
        int randTime = Random.Range(minTime, maxTime);
        speed = randSpeed;
        setTimer = randTime;
        timeLeft = setTimer;
       
    }
}
