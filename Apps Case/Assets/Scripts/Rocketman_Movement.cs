using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]private Vector3 move;
    private bool isAlive;
    public Button replayButton;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isGliding = false;
        eulerRotation = new Vector3(180*2, 0, 0);
        controller = GetComponent<CharacterController>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown&&isAlive)
        {
            this.transform.parent = null;
            if (Input.GetMouseButtonDown(0))
            {
                isGliding = true;
                animator.SetBool("isClicked",true);
                animator.Play("Armature|1_Open_wings_2");
                firstMousePos = Input.mousePosition.x;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90f, 90, 90), Time.time * 1f);
            }
            else if (Input.GetMouseButton(0))
            {
                
                movementAmount = Input.mousePosition.x - firstMousePos;
                movementAmount = Mathf.Clamp(movementAmount/20, -1, 1);
                move = new Vector3(movementAmount, 0, 0);
                move = (move + Vector3.forward+Physics.gravity/100);
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
        
        if (isGliding && isAlive)
        {
            Movement();
        }
        else if(isThrown && isAlive)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerRotation* Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation*deltaRotation);
        }
    }

    private void Movement()
    {
        rb.velocity = move * Time.deltaTime * playerSpeed*100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            isAlive = false;
            replayButton.gameObject.SetActive(true);
            rb.velocity = Vector3.zero;
        }
    }
}
