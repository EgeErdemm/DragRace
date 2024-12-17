using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBuySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buyingSound;



    public void BuySound()
    {
        StartCoroutine(PlaySoundForDuration(3f));
    }


    private IEnumerator PlaySoundForDuration(float duration)
    {



        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }


}
