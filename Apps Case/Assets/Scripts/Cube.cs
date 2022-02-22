using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float jumpAmount;

    private BoxCollider upCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 force = (Vector3.forward*10 + Vector3.up*jumpAmount).normalized;
        other.gameObject.GetComponent<Rigidbody>().AddForce(force*1000);
    }
}
