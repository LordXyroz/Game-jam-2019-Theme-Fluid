﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("InputManager")]
    public InputManager inputManager;

    [Header("Transforms")]
    public Transform myTransform;
    public Transform mainCamTransform;

    [Header("Components")]
    public Rigidbody rb;

    [Header("Other")]
    public Vector3 cameraOffset;
    public float movementSpeed;
    public float jumpForce;
    public float turnSpeed;
    public bool view;

    // Start is called before the first frame update
    void Start()
    {
        mainCamTransform.position = myTransform.position;

        view = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var temp = GetInstanceID();

        inputManager.RegisterAction(InputManager.Keys.forward, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.backward, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.left, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.right, () => Move(inputManager.data), temp);

        inputManager.RegisterAction(InputManager.Keys.jump, Jump, temp);
        inputManager.RegisterAction(InputManager.Keys.escape, ToggleView, temp);
    }
    
    public void Move(InputManager.CallbackData data)
    {
        var moveDir = Time.deltaTime * movementSpeed * myTransform.forward * data.xAxis + Time.deltaTime * movementSpeed * myTransform.right * data.yAxis;

        myTransform.position += moveDir;
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(0, 10f, 0));
    }

    public void ToggleView()
    {
        view = !view;
        Cursor.visible = !view;
        Cursor.lockState = view ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        if (view)
        {
            float y = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
            float x = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

            myTransform.Rotate(0f, y, 0f);
            mainCamTransform.Rotate(-x, 0f, 0f);

            cameraOffset = Quaternion.AngleAxis(y, Vector3.up) * cameraOffset;
            mainCamTransform.position = myTransform.position + cameraOffset;
        }
    }
}
