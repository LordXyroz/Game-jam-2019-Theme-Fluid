using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;

    [Header("Transforms")]
    public Transform myTransform;
    public Transform mainCamTransform;

    [Header("Components")]
    public Rigidbody rb;

    [Header("GameObjects")]
    public GameObject gun;

    [Header("Other")]
    public Vector3 cameraOffset;
    public float movementSpeed;
    public float jumpForce;
    public float turnSpeed;
    public bool view;
    public bool sprint;
    public bool gunDown;
    public bool move;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        mainCamTransform.position = myTransform.position;

        view = true;
        move = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var temp = GetInstanceID();

        inputManager.RegisterAction(InputManager.Keys.forward, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.backward, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.left, () => Move(inputManager.data), temp);
        inputManager.RegisterAction(InputManager.Keys.right, () => Move(inputManager.data), temp);

        inputManager.RegisterAction(InputManager.Keys.jump, Jump, temp);
        //inputManager.RegisterAction(InputManager.Keys.escape, ToggleView, temp);
        inputManager.RegisterAction(InputManager.Keys.sprint, Sprint, temp);
        inputManager.RegisterAction(InputManager.Keys.sprintUp, ReleaseSprint, temp);
        inputManager.RegisterAction(InputManager.Keys.stopMovement, StopMove, temp);

        float y = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        float x = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        myTransform.Rotate(0f, y, 0f);
        mainCamTransform.Rotate(-x, 0f, 0f);

        cameraOffset = Quaternion.AngleAxis(y, Vector3.up) * cameraOffset;
        mainCamTransform.position = myTransform.position + cameraOffset;
    }
    
    public void Move(InputManager.CallbackData data)
    {
        if (move)
        {
            var moveDir = Time.deltaTime * movementSpeed * myTransform.forward * data.xAxis + Time.deltaTime * movementSpeed * myTransform.right * data.yAxis;

            rb.velocity = moveDir * ((sprint) ? 75f : 50f) + new Vector3(0f, rb.velocity.y, 0f);
        }
        else
            StopMove();
    }

    public void StopMove()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    public void Jump()
    {
        if (move && grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            grounded = false;
        }
    }

    public void ToggleView()
    {
        if (move)
        {
            view = !view;
            Cursor.visible = !view;
            Cursor.lockState = view ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    public void Sprint()
    {
        if (move)
        {
            gunDown = true;
            sprint = true;
        }
    }

    public void ReleaseSprint()
    {
        if (move)
        {
            gunDown = false;
            sprint = false;
        }
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

        if (gunDown)
            gun.transform.localEulerAngles = new Vector3(20f, -60f, 0f);
        else
            gun.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wood" || collision.gameObject.tag == "Heavy" || collision.gameObject.tag == "SuperHeavy")
            grounded = true;
    }
    
}
