using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement_Opponent : MonoBehaviour
{

    public static Movement_Opponent Instance;

    private Rigidbody rb;

    public float accelerationPower;
    private float initialAccelerationPower;


    [SerializeField] private float carTopSpeed;
    private Vector3 maxVelocity;

    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {

       
        rb = GetComponent<Rigidbody>();
        initialAccelerationPower = accelerationPower;
        maxVelocity = new Vector3(0f, 0f, carTopSpeed);
    }



    void Update()
    {
        if(rb == null)
        {
            Debug.LogError("NUll rb");
        }
        if (rb.velocity.magnitude > maxVelocity.magnitude)
        {
            rb.velocity = maxVelocity;
        }
        //Debug.Log("opponent velocity" + rb.velocity.z);

    }

    private void FixedUpdate()
    {
        Gas();
    }


    public void Gas()
    {
        Vector3 forwardForce = transform.forward * accelerationPower;
        rb.AddForce(forwardForce, ForceMode.Acceleration);
    }

    public void SetAccelerationPower(int shift)
    {
        accelerationPower = initialAccelerationPower / shift;
    }

    public void VelocityReward(float rpmValue)
    {
        if(rpmValue>=350 && rpmValue<650)
        {
            //VelocityWithTime(10);
            StartCoroutine(VelocityRewardRoutine(1));
            Debug.Log("Rewarded Orange Line");

        }
        else if(rpmValue>=650 && rpmValue <= 700)
        {

            //VelocityWithTime(20);
            StartCoroutine(VelocityRewardRoutine(2));
            Debug.Log("Rewarded Green Line");
        }
        else if (rpmValue > 700)
        {
            StopAcceleration();
            Debug.Log("Stop Accelerate");

        }

    }

    public void StopAcceleration()
    {
        accelerationPower = 0f;
    }

    public void SetAcceleration(float accelaretion)
    {
        accelerationPower = accelaretion;
    }


    IEnumerator VelocityRewardRoutine(int velocityReward)
    {
        yield return new WaitForSeconds(1f);
        Vector3 velocity = rb.velocity;
        velocity.z = velocity.z + velocityReward;
        rb.velocity = velocity;
        //
        yield return new WaitForSeconds(1f);
        velocity = rb.velocity;
        velocity.z = velocity.z + velocityReward;
        rb.velocity = velocity;
        //     
        yield return new WaitForSeconds(1f);
        velocity = rb.velocity;
        velocity.z = velocity.z + velocityReward;
        rb.velocity = velocity;
        //   
        yield return new WaitForSeconds(1f);
        velocity = rb.velocity;
        velocity.z = velocity.z + velocityReward;
        rb.velocity = velocity;
        //        
        yield return new WaitForSeconds(1f);
        velocity = rb.velocity;
        velocity.z = velocity.z + velocityReward;
        rb.velocity = velocity;
    }



}
