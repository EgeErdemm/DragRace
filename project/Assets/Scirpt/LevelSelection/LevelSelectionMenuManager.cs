using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public static int CurrentLevel;


 

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickLevel(int levelNumb)
    {

        PlayerPrefs.SetInt("OpponentLevel", levelNumb);

        SceneManager.LoadScene(1);
    }



}