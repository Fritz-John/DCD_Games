using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public float health = 10f;
   
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    float speed = 10f;
    float timeCount = 0.0f;
    private void Start()
    {
   
        initialRotation = Quaternion.Euler(-90f, 0f, -90f); 
        targetRotation = Quaternion.Euler(0f, 0f, -90f); 
    }
    public void Damage(float damage)
    {
        health -= damage;
       
    }
    private void Update()
    {
        if (health <= 0)
        {
            
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, timeCount * speed);
            timeCount = timeCount + Time.deltaTime;

        }
    }
}
