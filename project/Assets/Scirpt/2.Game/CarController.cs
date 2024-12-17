using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public static CarController Instance { get; private set; }



    [SerializeField] private Transform fLeft;
    [SerializeField] private Transform fRight;
    [SerializeField] private Transform rLeft;
    [SerializeField] private Transform rRight;

    private Rigidbody rb;

    public float accelerateTime;
    private Vector3 acceleration = Vector3.zero;


    Vector3 left = new Vector3(-1f, 0f, 0f);
    Vector3 right = new Vector3(1f, 0f, 0f);


    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        acceleration = rb.velocity;


    }



    private void Update()
    {

        if (accelerateTime >= 0)
        {
            accelerateTime -= Time.deltaTime;

            acceleration.z = acceleration.z + Time.deltaTime * 10f;
            rb.velocity = acceleration;
        }


        if (isTurningLeft)
        {
            TurnLeft();
        }

        if (isTurningRight)
        {
            TurnRight();
        }


        rb.velocity = transform.forward * rb.velocity.magnitude;



     

    }





    public void TurnLeft()
    {
        if (transform.eulerAngles.y < 330f && transform.eulerAngles.y >270f) return;
        float x = left.x * Time.deltaTime *10;

        Vector3 angles = transform.eulerAngles;
        angles.y += 360+ x * 2f;
        transform.eulerAngles = angles;
        fLeft.localEulerAngles = angles + new Vector3(0f,-5f,0f);
        fRight.localEulerAngles = angles + new Vector3(0f, -5f,0f);

    }

    public void TurnRight()
    {
        if (transform.eulerAngles.y > 30f && !(transform.eulerAngles.y >= 270f && transform.eulerAngles.y <= 360f))
        {
            return;
        }

        float x = right.x * Time.deltaTime *10;
        if (x > 10) return;
        Vector3 angles = transform.eulerAngles;
        angles.y += x * 2f;
        transform.eulerAngles = angles;
        fLeft.localEulerAngles = angles + new Vector3(0f, 5f,0f);
        fRight.localEulerAngles = angles + new Vector3(0f, 5f, 0f);
    }







    public void StartTurningLeft()
    {
        isTurningLeft = true;
    }

    public void StopTurningLeft()
    {
        isTurningLeft = false;
    }

    public void StartTurningRight()
    {
        isTurningRight = true;
    }

    public void StopTurningRight()
    {
        isTurningRight = false;
    }










    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BoxCollider boxCollider))
        {
            Debug.Log("Game OVER");
        }
    }





}
