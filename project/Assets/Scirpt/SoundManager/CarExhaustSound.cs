using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExhaustSound : MonoBehaviour
{
    [SerializeField] private AudioClip carSound;
    private AudioSource audioSource;


    private void Start()
    {
        StartCoroutine(waitGameStart());
    }




    public void CarEngineSoundLoop()
    {
        audioSource.clip = carSound;
        audioSource.volume = 1f;
        audioSource.Play();
    }


    IEnumerator waitGameStart()
    {
        yield return new WaitForSeconds(3f);
        audioSource = GetComponent<AudioSource>();
        CarEngineSoundLoop();

    }



}
