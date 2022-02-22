using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public bool isThrown;
    [SerializeField] private float animTime;
    private Vector3 eulerRotation;
    private bool isGliding;
    [SerializeField] private float playerSpeed;
    private CharacterController controller;
    private float firstMousePos;
    [SerializeField] private float movementAmount;

    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isGliding = false;
        eulerRotation = new Vector3(180, 0, 0);
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown)
        {
            this.transform.parent = null;
            if (Input.GetMouseButtonDown(0))
            {
                
                isGliding = true;
                animator.SetBool("isClicked",true);
                animator.Play("Armature|1_Open_wings_2");
                firstMousePos = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                movementAmount = Input.mousePosition.x - firstMousePos;
                movementAmount = Mathf.Clamp(movementAmount/20, -3, 3);
                move = new Vector3(movementAmount, 0, 0);
                move = (move + Vector3.forward).normalized;
                animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                animTime = Mathf.Clamp(animTime, 0, 1);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.Play("Armature|2_Close_wings",0,1-animTime);
                animator.SetBool("isClicked",false);
                isGliding = false;
            }

        }
        
    }

    private void FixedUpdate()
    {
        
        if (isGliding)
        {
            
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.x,0,0), Quaternion.Euler(90f, 0, 0), Time.time * 0.5f);
            Movement();
        }
        else if(isThrown)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerRotation* Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation*deltaRotation);
        }
    }

    private void Movement()
    {
        rb.velocity = move * Time.deltaTime * playerSpeed*100;
    }
}
