using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour 
{
    public static ButtonHandler Instance { get; private set; }


    public bool GasPressed { get; private set; }
    public bool BrakePressed { get; private set; }
    public bool RightPressed { get; private set; }
    public bool LeftPressed { get; private set; }




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
    
    }




    public void OnPointerDown()
    {
        GasPressed = true;
    }

    public void OnPointerUp()
    {
        GasPressed = false;
    }



    public void OnPointerDownBrake()
    {
        BrakePressed = true;
    }

    public void OnPointerUpBrake()
    {
        BrakePressed = false;
    }


    public void OnPointerDownRight()
    {
        RightPressed = true;
    }

    public void OnPointerUpRight()
    {
        RightPressed = false;
    }



    public void OnPointerDownLeft()
    {
        LeftPressed = true;
    }

    public void OnPointerUpLeft()
    {
        LeftPressed = false;
    }


}
