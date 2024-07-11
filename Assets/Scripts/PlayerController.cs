using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Instance")]
    [SerializeField] public static PlayerController instance;

    [Header("InputAction")]
    [SerializeField] public InputActionReference moveAction;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [SerializeField] public float groundDrag;

    [SerializeField] public float walkSpeed;
    [SerializeField] public float sprintSpeed;

    [SerializeField] public Transform orientation;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    [SerializeField] public Vector3 moveDirection;
    [SerializeField] public Rigidbody rb;

    [Header("Animator")]
    [SerializeField] public Animator animator;

    [SerializeField] public Transform model;

    [Header("Combat")]
    [SerializeField] public Collider punchHand;

    //public GameObject headPlaceHolder;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    private void Start()
    {
        //animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        
        if(animator.GetBool("Punch") == true && animator.GetCurrentAnimatorStateInfo(0).length == 1.0f / animator.GetFloat("Speed"))
        {
            animator.SetBool("Punch", false);
        }

        Debug.Log(moveAction.action.ReadValue<Vector2>().y);
        Debug.Log(moveAction.action.ReadValue<Vector2>().x);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * moveAction.action.ReadValue<Vector2>().y + orientation.right * moveAction.action.ReadValue<Vector2>().x;

        //Debug.Log(moveAction.action.ReadValue<Vector2>().y);
        //Debug.Log(moveAction.action.ReadValue<Vector2>().x);

        if (moveDirection != Vector3.zero) 
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            model.forward = Vector3.Slerp(model.forward, moveDirection.normalized, Time.deltaTime * moveSpeed);
            animator.SetBool("Walk", true);
        } else
        {
            animator.SetBool("Walk", false);
        }    
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Punch()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Punch", true);
    }
}
