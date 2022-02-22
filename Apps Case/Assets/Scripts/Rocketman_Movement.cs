using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public bool isThrown;
    [SerializeField] private float animTime;
    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown)
        {
           // transform.Rotate(70*Time.deltaTime,0,0);
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("isClicked",true);
                animator.Play("Armature|1_Open_wings_2");
            }
            else if (Input.GetMouseButton(0))
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,0,0), Quaternion.Euler(90f, 0, 0), Time.time * 0.2f);
                animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.Play("Armature|2_Close_wings",0,1-animTime);
                animator.SetBool("isClicked",false);
            }
        }
        
    }
}
