using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    private Animator animator;

    [SerializeField]private float animTime;
    private float firstMousePos;
    [SerializeField] private float movementAmount;
    [SerializeField] private float min = 0f;
    [SerializeField] private float max = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            firstMousePos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            
            movementAmount = Input.mousePosition.x - firstMousePos;
            if (movementAmount <= 0)
            {
                movementAmount = Math.Abs(movementAmount);
                movementAmount = movementAmount / 100;
                animator.SetFloat("animTime",movementAmount);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            movementAmount = 1 - movementAmount;
            animator.Play("Armature|Release_Stick",0,movementAmount/4);
            

        }

        


    }
    
}
