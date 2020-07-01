using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigid;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = this.transform.up * speed;
    }
}
