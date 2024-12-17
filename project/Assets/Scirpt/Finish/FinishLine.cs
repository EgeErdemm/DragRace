using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System;

public class FinishLine : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI earnedMoneyWinTxt,earnedMoneyLoseTxt;
    [SerializeField] private GameObject winPanel, losePanel;
    [SerializeField] private GameObject winButton, loseButton;

    private LevelSaveUnlock levelSaveUnlock;

    public bool PlayerWin { get; private set; }

    public bool OpponentWin { get; private set; }

    private AsyncOperation loadSceneAsync;
    private int moneyEarned;

    int loseCount;
    GameObject rpmClock;
    GameObject shiftButton;



    void Start()
    {
        levelSaveUnlock = LevelSaveUnlock.Instance;
        PlayerWin = false;
        OpponentWin = false;
        loseCount = PlayerPrefs.GetInt("LoseCount", 0);
        rpmClock = GameObject.Find("RpmClock");
        shiftButton = GameObject.Find("ShiftButton");
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movement movement) && !OpponentWin)
        {

            rpmClock.SetActive(false);
            shiftButton.SetActive(false);
            Rigidbody rb = movement.GetComponent<Rigidbody>();
            Vector3 a = rb.velocity;
            a.z = a.z * 0.2f;
            rb.velocity = new Vector3(0f, 0f, a.z);

  
     

            StartCoroutine(ExecuteAfterDelay(1f, () =>
            {
                rb.velocity = new Vector3(0f, 0f, 0f);

                Destroy(rb);

            }));
            PlayerWin = true;
            PassedLevel(movement);
            Debug.Log("Player win");
            Time.timeScale = 1f;
            // money earing system
            int earnedMoney =PlayerPrefs.GetInt("OpponentLevel", 1) * 250 ;
            moneyEarned = earnedMoney;
            earnedMoney = PlayerPrefs.GetInt("Money", 0) + earnedMoney;
            PlayerPrefs.SetInt("Money", earnedMoney);
            //
            StartCoroutine(PassToMainScene());
            //LoseCOntrol
            PlayerPrefs.SetInt("LoseCount",0);


            //

        }else if (OpponentWin)
        {
            movement.enabled = false;
            Rigidbody rb = movement.GetComponent<Rigidbody>();
            Vector3 a = rb.velocity;
            a.z = a.z * 0.5f;
            rb.velocity = new Vector3(0f, 0f, a.z);

            a = rb.velocity;
            a.z = a.z * 0.5f;
            StartCoroutine(ExecuteAfterDelay(0.5f, () =>
            {
                rb.velocity = new Vector3(0f, 0f, a.z);


            }));

            StartCoroutine(ExecuteAfterDelay(2f, () =>
            {
                rb.velocity = new Vector3(0f, 0f, 0f);

                Destroy(rb);

            }));
        }


        if (other.TryGetComponent(out Movement_Opponent movement_Opponent) && !PlayerWin)
        {
            rpmClock.SetActive(false);
            shiftButton.SetActive(false);

            Rigidbody rb = movement_Opponent.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0f, 0f, 10f);
            StartCoroutine(ExecuteAfterDelay(2f, () =>
            {
                rb.velocity = new Vector3(0f, 0f, 0f);

                Destroy(rb);

            }));
            OpponentWin = true;
            Debug.Log("Opponent win");
            Time.timeScale = 1f;
            // money earing system
            int earnedMoney = PlayerPrefs.GetInt("OpponentLevel", 1) * 50;
            moneyEarned = earnedMoney;
            earnedMoney = PlayerPrefs.GetInt("Money", 0) + earnedMoney;
            PlayerPrefs.SetInt("Money", earnedMoney);
            //
            StartCoroutine(PassToMainScene());
            //Lose Control
            loseCount++;
            PlayerPrefs.SetInt("LoseCount", loseCount);
  
            //
        }else if(PlayerWin)
        {
            Rigidbody rb = movement_Opponent.GetComponent<Rigidbody>();
            movement_Opponent.enabled = false;
            rb.velocity = new Vector3(0f, 0f, 5f);

            StartCoroutine(ExecuteAfterDelay(2f, () =>
            {
                rb.velocity = new Vector3(0f, 0f, 0f);

                Destroy(rb);

            }));
        }

   

    }



    public void PassedLevel(Movement movement)
    {
        // rakip seviyesini levelSelection da veriyoruz
        if (PlayerPrefs.GetInt("OpponentLevel", 1)>= movement.PlayerLevel)
        {
            movement.PlayerLevelUp();
            PlayerPrefs.SetInt("PlayerLevel", movement.PlayerLevel);
        }
       

        int level = movement.PlayerLevel;
        string levelString = level.ToString();
        PlayerPrefs.SetInt(levelString, 1);
   
        

    }


    public void WinLoseCaseText()
    {
        if (PlayerWin)
        {
            winPanel.SetActive(true);
            earnedMoneyWinTxt.text = "+" + moneyEarned.ToString();
            StartCoroutine(ShakeButton(winButton));
        }
        if (OpponentWin)
        {
            losePanel.SetActive(true);
            earnedMoneyLoseTxt.text ="+" + moneyEarned.ToString();
            StartCoroutine(ShakeButton(loseButton));

        }
    }
 


    IEnumerator PassToMainScene()
    {
        Debug.Log("Someone Win");
        loadSceneAsync = SceneManager.LoadSceneAsync(0);
        WinLoseCaseText();
        loadSceneAsync.allowSceneActivation = false;
        yield return null;
    }

    IEnumerator ShakeButton(GameObject button)
    {
        yield return new WaitForSeconds(3f);
        button.transform.DOShakeScale(40f, 0.04f, 20, 360);

    }


    public void ToMainMenu()
    {
        loadSceneAsync.allowSceneActivation = true;
    }



    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
