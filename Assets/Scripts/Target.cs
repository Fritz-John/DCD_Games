using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public float health;
   
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public GameObject targetToRotate;

    public float speed;

    float timeCount = 0.0f;

    public bool alive = false;

    public float timeLeft;
    public float minTime;
    public float maxTime;
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
        if(target != null)
        {
            target.RemoveTargetAtList(gameObject);
        }
       
    }

    private void Update()
    {
        //target = FindObjectOfType<SpawnerTarget>();
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
            else
            {

                alive = false;
                timeCount = 0.0f;
                timeLeft = setTimer;
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
        float randSpeed = Random.Range(7f, 10f);
        float randTime = Random.Range(minTime, maxTime);
        speed = randSpeed;
        setTimer = randTime;
        
       
    }
}
