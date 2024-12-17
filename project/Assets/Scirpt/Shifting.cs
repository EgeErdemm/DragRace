using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Shifting : MonoBehaviour
{


    private ButtonHandler buttonHandler;
    private Movement movement;
    private ShiftingText shiftingText;

    private TextMeshProUGUI shiftCountTxt;


    [SerializeField] private RectTransform rpmClock;

    [SerializeField] private float rpmIncreasePower;

    [SerializeField] private float[] shiftingLineBorder;
    public int ShiftCount { get; private set; }

    [SerializeField] private Button shiftButton;
    [SerializeField] private TextMeshProUGUI shiftAccelerateText;
    bool glow= false;

    private int maxShiftCount = 5;

    private bool redAlertflag = true;


    [SerializeField] private RectTransform Ibre;
    [SerializeField] private Image shiftArrow;
    private Coroutine coroutine;
    bool CR_running;
    bool boolForFirstShift=false;


    void Start()
    {
        GameObject ShiftCountTextObject = GameObject.Find("Count");
        shiftCountTxt = ShiftCountTextObject.GetComponent<TextMeshProUGUI>();
        buttonHandler = ButtonHandler.Instance;
        movement = Movement.Instance;
        shiftingText = ShiftingText.Instance;

        ShiftCount = 1;
        StartCoroutine(GlowEffect());

        StartCoroutine(ExecuteAfterDelay((4.5f), () =>
        {
            boolForFirstShift = true;
        }));

    }


    void Update()
    {
      


        //Ibre
        if (Ibre.eulerAngles.z <190)
        {
            if (movement == null)
                return;
            movement.StopAcceleration();

            shiftAccelerateText.color = Color.red;
            shiftAccelerateText.text = "Acceleration level: 0";


            if (ShiftCount < maxShiftCount)
            {
                shiftingText.RedShiftingText();
                glow = true;
                redAlertflag = true;
                StartCoroutine(RedAlert());

            }

        }
        else
        {
            shiftAccelerateText.color = Color.white;
            glow = false;
            redAlertflag = false;
            StopCoroutine(RedAlert());
            shiftingText.KillTweensRed();
        }

        if (Ibre.eulerAngles.z > 270)
        {
            shiftArrow.color = Color.white;

     
        }
        else if (Ibre.eulerAngles.z <= 270 & Ibre.eulerAngles.z > 210)
        {
            if(!CR_running)
                coroutine = StartCoroutine(ShiftArrowColorWave(new Color(0.89f, 0.36f, 0.09f)));


        }
        else if (Ibre.eulerAngles.z <= 210 & Ibre.eulerAngles.z >= 190)
        {
            if (!CR_running)
                coroutine = StartCoroutine(ShiftArrowColorWave(new Color(0f, 0.9f, 0.05f)));

            new Color(0f, 0.9f, 0.05f);
     
        }
        else if (Ibre.eulerAngles.z < 190)
        {
            if (!CR_running)
                coroutine = StartCoroutine(ShiftArrowColorWave(Color.red));

        }

        //


    }

    private void FixedUpdate()
    {
        if (buttonHandler != null)
        {
            if (buttonHandler.GasPressed)
            {
            }
            else
            {
                DecreaseRpm();
            }
        }
        IncreaseRpm();
        IncreaseIbre();

    }



    public void ChangeShift()
    {

        if (!boolForFirstShift)
        {
            return;
        }

        if (ShiftCount >= maxShiftCount)
        {
            shiftCountTxt.color = Color.red;
            StartCoroutine(ExecuteAfterDelay(0.5f, () =>
            {
                shiftCountTxt.color = Color.white;
            }));
            return;

        }


        ShiftCount++;
        shiftCountTxt.text = ShiftCount.ToString();
        int accelerationLevel = maxShiftCount - ShiftCount +1;
        shiftAccelerateText.text = "Acceleration level: " + accelerationLevel.ToString();

        ShiftingDetection();
        movement.SetAccelerationPower(ShiftCount);
        if (rpmClock.anchoredPosition.y > 350f)
        {
            Vector3 rpm = new Vector3(0f, rpmClock.anchoredPosition.y / 3f, 0);
            rpmClock.anchoredPosition = rpm;
        }

        //Ibre rotation shifting
        Vector3 IbreRotation = Ibre.eulerAngles;
        float z = (360f - IbreRotation.z) * 0.5f;
        IbreRotation.z = IbreRotation.z + z +20;
        Ibre.eulerAngles = IbreRotation;
        //
        Debug.Log(ShiftCount);

        boolForFirstShift = false;
        StartCoroutine(ExecuteAfterDelay((0.5f), () =>
        {
            boolForFirstShift = true;
        }));
    }

    private void ShiftingDetection()
    {
        int lastBorder = 0;

        foreach (float border in shiftingLineBorder)
        {
            if (rpmClock.anchoredPosition.y < border)
            {
                break;
            }

            lastBorder++;

        }

        //movement.VelocityReward(lastBorder);

        Debug.Log(lastBorder);

        Vector3 IbreRotation = Ibre.eulerAngles;
        float IbreZ = IbreRotation.z;
        ShiftingCaseTexting(lastBorder,IbreZ);

    }

    private void ShiftingCaseTexting(int lastBorder, float IbreZ)
    {


        //Ibre
        if (IbreZ>270)
        {
            shiftingText.GreyShiftingText();
            movement.VelocityReward(0);

        }
        else if (IbreZ <= 270 & IbreZ > 210)
        {
            shiftingText.OrangeShiftingText();
            movement.VelocityReward(1);


        }
        else if (IbreZ<=210 & IbreZ >=190)
        {
            shiftingText.GreenShiftingText();
            movement.VelocityReward(2);

        }
        else if(IbreZ<190)
        {
            shiftingText.ClearShiftingText();
        }

        //

    }

    private void IncreaseRpm()
    {

        Vector3 rpmPos = rpmClock.anchoredPosition;
        rpmPos.y = rpmClock.anchoredPosition.y + rpmIncreasePower * Time.fixedDeltaTime; // increases by 4 units per frame 1sec=200
        if (rpmClock.anchoredPosition.y >= 870) rpmPos.y = 870f;

        rpmClock.anchoredPosition = rpmPos;
    }

    private void IncreaseIbre()
    {
        Vector3 IbreRotation = Ibre.eulerAngles;
        IbreRotation.z = IbreRotation.z -0.9f;
        if (IbreRotation.z <= 128) IbreRotation.z = 131f;
        Ibre.eulerAngles = IbreRotation;

    }



    private void DecreaseRpm()
    {

        Vector3 rpmPos = rpmClock.anchoredPosition;
        rpmPos.y = rpmClock.anchoredPosition.y - Time.fixedDeltaTime * rpmIncreasePower / 10f;
        if (rpmClock.anchoredPosition.y <= 0) rpmPos.y = 0f;
        rpmClock.anchoredPosition = rpmPos;

    }



    //Shifting Button Glow
 

    IEnumerator GlowEffect()
    {
        Image buttonImage = shiftButton.GetComponent<Image>();
        Color originalColor = buttonImage.color;
        while (true)
        {
            if (glow)
            {
                buttonImage.color = shiftButton.colors.pressedColor;

                yield return new WaitForSeconds(0.1f);

                buttonImage.color = originalColor;

                yield return new WaitForSeconds(0.1f);

                Debug.Log("glow");
            }
            else
            {
                yield return null;
            }
         
        }
      



    }

    //


    IEnumerator RedAlert()
    {
        yield return new WaitForSeconds(1f);
        shiftingText.RedTweenStart();
    }

    IEnumerator ShiftArrowColorWave(Color color)
    {

        CR_running = true;
        shiftArrow.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        shiftArrow.color = color;
        yield return new WaitForSeconds(0.1f);
        CR_running = false;




    }


    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke(); // Verilen lambdayı çağırır.
    }


}