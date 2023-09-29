using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        yRot = Mathf.Clamp(yRot, -55f, 55f);

      
        Quaternion newRotation = Quaternion.Euler(xRot, yRot, 0f);

       
        cameraTransform.rotation = newRotation;
    }
}
