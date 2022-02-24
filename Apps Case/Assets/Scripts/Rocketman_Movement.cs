using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Rocketman_Movement : MonoBehaviour
{
    [SerializeField] private float movementAmount;
    [SerializeField] private Vector3 move;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float animTime;
    
    private bool isGliding;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 eulerRotation;
    private float firstMousePos;
    private bool isAlive;
    
    public bool isThrown;
    public Button replayButton;
    public GameObject rotater;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isThrown = false;
        isGliding = false;
        eulerRotation = new Vector3(180*4, 0, 0);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown&&isAlive)
        {
            this.transform.parent.parent = null;
            if (Input.GetMouseButtonDown(0))
            {
                isGliding = true;
                animator.SetBool("isClicked",true);
                animator.Play("Armature|1_Open_wings_2");
                firstMousePos = Input.mousePosition.x;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90f, 0, 0), Time.time * 1f);
            }
            else if (Input.GetMouseButton(0))
            {
                movementAmount = Input.mousePosition.x - firstMousePos;
                movementAmount = Mathf.Clamp(movementAmount/20, -2, 2);
                move = new Vector3(movementAmount, 0, 0);
                move = (move + Vector3.forward*3+Physics.gravity/20);
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
            rotater.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, -movementAmount*5, 0), Time.time * 0.2f);
        }
        else if(isThrown && isAlive)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerRotation* Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation*deltaRotation);
            Vector3 grav = Physics.gravity/10 + rb.velocity;
            rb.velocity = grav;
        }
    }

    private void Movement()
    {
        rb.velocity = move * Time.deltaTime * playerSpeed*100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            isAlive = false;
            replayButton.gameObject.SetActive(true);
            rb.velocity = Vector3.zero;
        }
    }
}
