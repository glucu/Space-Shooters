using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed;
    Rigidbody2D rid;

    private void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        rid.velocity = transform.up * laserSpeed;
    }
}
