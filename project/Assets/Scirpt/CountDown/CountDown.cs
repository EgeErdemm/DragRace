using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject ShiftingSCObject;
    private GameObject player, opponent;

    private void Start()
    {
        StartCoroutine(waitForInstantiate());
    }



    IEnumerator waitForInstantiate()
    {
        yield return null;
        player = GameObject.FindWithTag("Player");
        opponent = GameObject.FindWithTag("Opponent");

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Rigidbody oRb = opponent.GetComponent<Rigidbody>();
        oRb.velocity = Vector3.zero;



        player.GetComponent<Movement>().enabled = false;
        ShiftingSCObject.GetComponent<Shifting>().enabled = false;
        opponent.GetComponent<Movement_Opponent>().enabled = false;
        opponent.GetComponent<Shift_Opponent>().enabled = false;

        StartCoroutine(StartCountdown());
    }


    IEnumerator StartCountdown()
    {
        Debug.Log("COuntDOwn");
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "DRAG ON";




        player.GetComponent<Movement>().enabled = true;
        ShiftingSCObject.GetComponent<Shifting>().enabled = true;
        opponent.GetComponent<Movement_Opponent>().enabled = true;
        opponent.GetComponent<Shift_Opponent>().enabled = true;

        yield return new WaitForSeconds(1f);
        countDownText.text = "";


    }

}
