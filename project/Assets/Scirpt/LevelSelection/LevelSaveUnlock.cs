using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSaveUnlock : MonoBehaviour
{

    public static LevelSaveUnlock Instance;

    [SerializeField] private Transform content; // buttons are inside in it
    [SerializeField] private Transform content1;
    [SerializeField] private Transform content2;

    private Button[] buttons;
    private Button[] buttons1;
    private Button[] buttons2;


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








    void Start()
    {
        buttons = content.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i + 1;

            if (PlayerPrefs.GetInt(index.ToString()) == 1) // if i passed the level getint =1
            {
                Button button = buttons[i];
                button.interactable = true;
            }

        }

        buttons1 = content1.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i + 11;

            if (PlayerPrefs.GetInt(index.ToString()) == 1) // if i passed the level getint =1
            {
                Button button = buttons1[i];
                button.interactable = true;
            }

        }


        buttons2 = content2.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i + 21;

            if (PlayerPrefs.GetInt(index.ToString()) == 1) // if i passed the level getint =1
            {
                Button button = buttons2[i];
                button.interactable = true;
            }

        }

    }




}
