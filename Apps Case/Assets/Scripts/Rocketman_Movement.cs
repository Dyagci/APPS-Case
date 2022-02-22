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
    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isGliding = false;
        eulerRotation = new Vector3(100, 0, 0);
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
            }
            else if (Input.GetMouseButton(0))
            {
                
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
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.x,0,0), Quaternion.Euler(90f, 0, 0), Time.time * 0.1f);
        }
        else if(isThrown)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerRotation* Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation*deltaRotation);
            //transform.Rotate(70*Time.deltaTime,0,0);
        }
    }
}
