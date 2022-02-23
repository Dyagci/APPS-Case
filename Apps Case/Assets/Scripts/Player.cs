using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xDistanceTraveled;
    public float zDistanceTraveled;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xDistanceTraveled = startPos.x - transform.position.x;
        zDistanceTraveled = startPos.z - transform.position.z;
    }
}
