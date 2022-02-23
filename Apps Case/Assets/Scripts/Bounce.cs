using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float jumpAmount;

    private BoxCollider upCollider;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 force = (Vector3.forward*10 + Vector3.up*jumpAmount).normalized;
        other.gameObject.GetComponent<Rigidbody>().AddForce(force*2500);
    }
}
