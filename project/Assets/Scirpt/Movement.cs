using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public static Movement Instance;

    private ButtonHandler buttonHandler;

    private Rigidbody rb;

    private TextMeshProUGUI speedTxt;



    public float AccelerationPower;
    public int PlayerLevel { get; private set; }

    private float initialAccelerationPower;

    [SerializeField] private float carTopSpeed;
    private Vector3 maxVelocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(Instance);
        }
    }



    void Start()
    {

        GameObject SpeedText = GameObject.Find("SpeedTxt");
        speedTxt = SpeedText.GetComponent<TextMeshProUGUI>();

        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);

        buttonHandler = ButtonHandler.Instance;
        rb = GetComponent<Rigidbody>();
        initialAccelerationPower = AccelerationPower;
        maxVelocity = new Vector3(0f, 0f, carTopSpeed);
    }


    void Update()
    {
        speedTxt.text = (int)rb.velocity.magnitude + "/Kmp";
        if (rb.velocity.magnitude > maxVelocity.magnitude)
        {
            rb.velocity = maxVelocity;
        }
    }

    private void FixedUpdate()
    {
        if (buttonHandler.GasPressed)
        {
        }
        Gas();

    }


    public void Gas()
    {
        Vector3 forwardForce = transform.forward * AccelerationPower;
        rb.AddForce(forwardForce, ForceMode.Acceleration);
    }


    public void SetAccelerationPower(int shift)
    {
        AccelerationPower = initialAccelerationPower / shift;
    }

    public void VelocityReward(int border)
    {
        if(border < 3)
        {
            //Vector3 velocity = rb.velocity;
            //velocity.z = velocity.z + 5 * border;
            //rb.velocity = velocity; 
            //VelocityWithTime(5 * border);
            StartCoroutine(VelocityRewardRoutine(border));
        }
    }

    public void StopAcceleration()
    {
        AccelerationPower = 0f;
    }



    public void PlayerLevelUp()
    {
        PlayerLevel++;
    }

    public void PlayerAcceleration(float acceleration)
    {
        AccelerationPower = acceleration;
    }


    public void UpgradeTopSpeed(int upgradeLevel)
    {
        carTopSpeed += upgradeLevel * 5;
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
