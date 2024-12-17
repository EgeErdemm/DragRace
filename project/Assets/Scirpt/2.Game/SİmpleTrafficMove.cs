using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrafficMove : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 25;
    }



}
