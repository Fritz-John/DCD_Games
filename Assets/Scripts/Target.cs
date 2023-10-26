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
    private Quaternion tryTargetRotation;

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

    float targetFloat;

    public bool isSelected = false;
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

        currentTimeAlive.text = timeLeft.ToString();
        timeCount += Time.deltaTime;
        if (health <= 0)
        {
            tryTargetRotation = Quaternion.Euler(0f, 0f, 90f);
            targetToRotate.transform.localRotation = Quaternion.Lerp(targetToRotate.transform.localRotation, tryTargetRotation, timeCount * speed);
            alive = false;
        }
        else if (health > 0)
        {
            tryTargetRotation = Quaternion.Euler(-90f, 0f, 90f);
            targetToRotate.transform.localRotation = Quaternion.Lerp(targetToRotate.transform.localRotation, tryTargetRotation, timeCount * speed);
            alive = true;

        }

        if (alive)
        {
            if (timeLeft > 0)
            {

                timeLeft -= Time.deltaTime;

            }
            else if (timeLeft <= 0)
            {

                alive = false;
                timeCount = 0.0f;
                setSpeed();
                health = 0;
                PlayerMovement player = FindObjectOfType<PlayerMovement>();
                 player.MinusHealth();
                

                if (target != null)
                {
                    target.RemoveTargetAtList(gameObject);
                }
                //target.RemoveTargetAtList(gameObject);

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
