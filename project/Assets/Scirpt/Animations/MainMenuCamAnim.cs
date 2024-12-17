using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MainMenuCamAnim : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    void Start()
    {
        Vector3 camPos;
        camPos = cameraTransform.position;
        camPos.z = -50f;
        cameraTransform.position = camPos;

        cameraTransform.DOMoveZ(730, 40).SetEase(Ease.Linear);


        StartCoroutine(ExecuteAfterDelay(40f, () =>
        {
            cameraTransform.position = camPos;
            cameraTransform.DOMoveZ(730, 40).SetEase(Ease.Linear);

        }));

    }
    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
