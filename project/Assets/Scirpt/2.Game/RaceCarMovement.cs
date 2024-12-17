using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCarMovement : MonoBehaviour 
{
    private ButtonHandler buttonHandler;


    private Rigidbody rb;
    public float AccelerationPower;


    void Start()
    {
        buttonHandler = ButtonHandler.Instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (buttonHandler.GasPressed)
        {
            Debug.Log("Gas");
            Gas();
        }
        else
        {
            Debug.Log("No gas");
        }

        if (buttonHandler.BrakePressed)
        {
            Debug.Log("Brake");

            Brake();
        }


        if (buttonHandler.RightPressed)
        {
            Vector3 angles = transform.eulerAngles;
            angles.y += Time.fixedDeltaTime;
            if (angles.y >= 10f)
                angles.y = 10f;

            transform.eulerAngles = angles;
        }


        if (buttonHandler.LeftPressed)
        {
            Vector3 angles = transform.eulerAngles;
            angles.y -= Time.fixedDeltaTime;
            if (angles.y <= 350f)
                angles.y = 350f;

            transform.eulerAngles = angles;
        }

    }







    public void Gas()
    {
        Vector3 forwardForce = transform.forward * AccelerationPower;
        rb.AddForce(forwardForce, ForceMode.Acceleration);
    }


    public void Brake()
    {
        Vector3 forwardForce = -transform.forward * AccelerationPower;
        rb.AddForce(forwardForce, ForceMode.Acceleration);
    }


}
