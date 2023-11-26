using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    PlayerInput controls;
    InputAction move;
    InputAction left;
    InputAction right;
    InputAction open;

    Animator doorAnim;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float turnAngle = 1;


    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public bool canOpen = false;

    //GameObject myCam;

    [SerializeField] GameObject openControlText;

    // Start is called before the first frame update
    void Start()
    {
        //myCam = GameObject.Find("MainCamera");
        controls = GetComponent<PlayerInput>();
        left = controls.actions.FindAction("Left");
        left.Enable();
        right = controls.actions.FindAction("Right");
        right.Enable();
        move = controls.actions.FindAction("Move");
        move.Enable();
        open = controls.actions.FindAction("Jump");
        open.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canOpen)
        {
            openControlText.SetActive(false);
            open.Disable();
        }
        if (left.IsPressed())
        {
            Vector3 newAngle = new Vector3(0, -turnAngle, 0);
            transform.eulerAngles += newAngle;
        }
        if (right.IsPressed())
        {
            Vector3 newAngle = new Vector3(0, turnAngle, 0);
           transform.eulerAngles += newAngle;
        }
        if (open.IsPressed())
        {
            doorAnim.SetBool("character_nearby", true);
            //doorAnim.gameObject.GetComponent<MeshCollider>().enabled = false;
        };

        if (move.IsInProgress())
        {
            Vector3 moveForward = transform.TransformDirection(Vector3.forward) * move.ReadValue<Vector2>().y;
            Vector3 moveLeft = transform.TransformDirection(Vector3.right) * move.ReadValue<Vector2>().x;

            Vector3 moveDirection = moveForward + moveLeft;

            GetComponent<Rigidbody>().MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorAnim = other.GetComponent<Animator>();
        if (other.CompareTag("Elevator"))
        {
            //other.GetComponent<Elevator>().readyToOpen = true;
            openControlText.SetActive(true);
            open.Enable();
            canOpen = true;
            other.GetComponent<Elevator>().readyToOpen = true;
        }
        else if (other.CompareTag("Door"))
        {
            //other.GetComponent<DoorControl>().readyToOpen = true;
            openControlText.SetActive(true);
            canOpen = true;
            open.Enable();
            other.GetComponent<DoorControl>().readyToOpen = true;
        }
    }
}
