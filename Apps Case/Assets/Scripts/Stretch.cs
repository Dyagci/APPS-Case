using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    private Animator animator;
    private float firstMousePos;
    [SerializeField] private float movementAmount;
    [SerializeField] private float min = 0f;
    [SerializeField] private float max = 1f;
    [SerializeField] private CinemachineVirtualCamera stickCam;
    [SerializeField] private CinemachineVirtualCamera playerCam;
    public GameObject player;
    

    private bool isThrown;
    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isThrown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePos = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                movementAmount = Input.mousePosition.x - firstMousePos;
                if (movementAmount <=0)
                {
                    movementAmount = Math.Abs(movementAmount);
                    movementAmount = movementAmount / 100;
                    movementAmount = Mathf.Clamp(movementAmount, min, max);
                    animator.SetFloat("animTime",movementAmount);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                movementAmount = 1 - movementAmount;
                animator.Play("Armature_Release_Stick",0,movementAmount/4);
            }
        }
    }

    public void ThrowPlayer()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().AddForce(Vector3.forward*2000);
        player.GetComponent<Rocketman_Movement>().isThrown = true;
        stickCam.Priority = 0;
        playerCam.Priority = 1;
        isThrown = true;

    }
    
}
