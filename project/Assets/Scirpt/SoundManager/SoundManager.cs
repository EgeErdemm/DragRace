using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip toRacesMenuSound;
    [SerializeField] private AudioClip[] bgMusics;

    private int queue=0;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if(Instance !=null & Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

   
    

    private void Start()
    {
        
        PlayBGMusics();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            queue++;
            if (queue >= bgMusics.Length)
                queue = 0;
            PlayBGMusics();

        }

        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    CarEngineSoundLoop();
        //    Debug.Log("EngineLoopActiveted");
        //}
        //else
        //{
        //    audioSource.Stop();
        //}

    }



    public void ToRacesMenuSound()
    {
        audioSource.clip = toRacesMenuSound;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void PlayBGMusics()
    {
        audioSource.clip = bgMusics[queue];
        audioSource.volume = 0.03f;
        audioSource.Play();
    }

   


}
