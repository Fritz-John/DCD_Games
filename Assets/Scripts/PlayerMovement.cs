using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public Transform cameraTransform;
    [Header("Camera Movement")]
    public float lookSensitivity = 2f;
    public float smoothTime = 0.1f;
    private Vector2 PlayerMouseInput;
    private float xRot;
    private float yRot;
    private float xRotVelocity = 0f;

    public int health = 5;
    public Text textHealth;

    public bool difficult;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        textHealth.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerCamera();
    }
    void MovePlayerCamera()
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        
        xRot -= PlayerMouseInput.y * lookSensitivity;
        xRot = Mathf.Clamp(xRot, -55f, 55f);



        yRot += PlayerMouseInput.x * lookSensitivity;
       

        if (!difficult)
        {
            
            yRot = Mathf.Clamp(yRot, -70f, 70f);
        }
        Quaternion newRotation = Quaternion.Euler(xRot, yRot, 0f);

       
        cameraTransform.rotation = newRotation;
    }
    public void MinusHealth()
    {
        health--;
        textHealth.text = health.ToString();    
    }
}
