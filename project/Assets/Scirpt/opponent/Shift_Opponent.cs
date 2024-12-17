using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shift_Opponent : MonoBehaviour
{
    private Movement_Opponent movement_Opponent;


    public int ShiftCount { get; private set; }

    private int maxShiftCount = 5;

    public float opponentLevel { get; private set; } // every level = 1/2 sec
    private float shiftingTime;
    private float baseShiftingTime = 1f;
    private float shiftingRpmValue;

    private float stopAccelerateTime;

    void Start()
    {
        movement_Opponent = Movement_Opponent.Instance;

        ShiftCount = 1;
        shiftingRpmValue = ShiftingRpm(); // Assume level = 1 and baseTime = 1, (level / 2 + baseTime) * 200 = 300
        stopAccelerateTime = (baseShiftingTime + opponentLevel / 2f);
    }



    private void Update()
    {
        shiftingTime -= Time.deltaTime;
        if (shiftingTime <= 0)
        {
            Shift();
            ShiftingRpm();
            movement_Opponent.VelocityReward(shiftingRpmValue);
        }
        if (ShiftCount >= maxShiftCount)
        {
            stopAccelerateTime -= Time.deltaTime;   
            if (stopAccelerateTime<= 0)
            {
                movement_Opponent.StopAcceleration();
                Debug.Log("Stoping accelerate");
            }

        }
    }

    private float ShiftingRpm()
    {
        shiftingTime = baseShiftingTime + opponentLevel / 2f;
        Debug.Log(opponentLevel);
        return shiftingTime * 200;
    }

    public void Shift()
    {
        if (ShiftCount >= maxShiftCount) return;

        ShiftCount++;
        movement_Opponent.SetAccelerationPower(ShiftCount);
        Debug.Log("Opponent shift count" + ShiftCount);

    }




    public void SetOpponentLevel(int level)
    {
        opponentLevel = level;
    }


}
