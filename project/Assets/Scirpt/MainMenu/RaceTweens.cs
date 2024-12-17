using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class RaceTweens : MonoBehaviour
{
    [SerializeField] private Transform PlayerPos, OpponentPos;
    [SerializeField] private GameObject[] Vehicals;
    private GameObject player, opponent;
    float time;

    private void Start()
    {
        time = Random.Range(25, 40);
        RaceTween();
        StartCoroutine(ExecuteAfterDelay(time, () =>
        {
            RaceTween();
        }));
      

    }

    private void RaceTween()
    {
        Debug.Log("RaceTween");
        int i = Random.Range(0, Vehicals.Length);
        int o = Random.Range(0, Vehicals.Length);
        player = Instantiate(Vehicals[i], PlayerPos);
        opponent = Instantiate(Vehicals[o], OpponentPos);


        player.transform.DOMoveZ(900, time).SetEase(Ease.Linear)
            .OnComplete( ()=>{

            Destroy(player);
        });
        opponent.transform.DOMoveZ(900, time - 2).SetEase(Ease.Linear)
            .OnComplete(() =>
        {
            Destroy(opponent);
        });
    }




    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke(); 
    }

}


