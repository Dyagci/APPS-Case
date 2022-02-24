using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float jumpAmount;
    private void OnTriggerEnter(Collider other)
    {

            Vector3 force = (Vector3.forward*10 + Vector3.up*jumpAmount);
            other.gameObject.GetComponent<Rigidbody>().AddForce(force*6,ForceMode.Impulse);
            Debug.Log("alo");

    }
}
