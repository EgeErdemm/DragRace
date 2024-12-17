using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionFinal : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private GameObject SlowMoCamera;
    private GameObject[] mainCamera;
    [SerializeField] private GameObject[] uiItem;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Movement movement))
        {
            CloseAllNotNeccaryUI();

            boxCollider.enabled = false;
            mainCamera[0].SetActive(false);
            SlowMoCamera.SetActive(true);
            StartCoroutine(DoSmoothSlowMotion());
        }
        else if (other.TryGetComponent(out Movement_Opponent movement_Opponent))
        {
            CloseAllNotNeccaryUI();

            boxCollider.enabled = false;
            mainCamera[0].SetActive(false);
            SlowMoCamera.SetActive(true);
            StartCoroutine(DoSmoothSlowMotion());
        }
    }

 

    IEnumerator DoSmoothSlowMotion()
    {
        bool completed = false;
        while (!completed)
        {
            Time.timeScale = 0.85f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0.7f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0.55f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0.4f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0.25f;
            completed = true;

        }
    }


    private void CloseAllNotNeccaryUI()
    {
        foreach (GameObject ui in uiItem)
        {
            ui.SetActive(false);
        }
    }
}
