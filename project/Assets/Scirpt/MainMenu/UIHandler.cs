using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu, ShopMenu, LevelSelectionMenu;
    [SerializeField] private Button[] purchasedCarBuyButton;
    [SerializeField] private TextMeshProUGUI moneyText;

    private SoundManager soundManager;

    [SerializeField] private GameObject textOfGameObject;
    private TextMeshProUGUI upgradeText;

    private int money;

    private void Start()
    {
        soundManager = SoundManager.Instance;
       // upgradeText = textOfGameObject.GetComponent<TextMeshProUGUI>();

        PurchasedCars();
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText.text = ""+PlayerPrefs.GetInt("Money", 0).ToString();

    }

    private void Update()
    {
        if(money != PlayerPrefs.GetInt("Money", 0))
        {
            moneyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
        }
    }

    public void ToMainMenu()
    {
        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
        LevelSelectionMenu.SetActive(false);
    }

    public void ToShopMenu()
    {
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
        LevelSelectionMenu.SetActive(false);
    }

    public void ToLevelSelectionMenu()
    {
        //
        soundManager.ToRacesMenuSound();
        //
        MainMenu.SetActive(false);
        ShopMenu.SetActive(false);
        LevelSelectionMenu.SetActive(true);
    }


    private void PurchasedCars()
    {
        for (int i = 0; i < purchasedCarBuyButton.Length; i++)
        {
            if(i< PlayerPrefs.GetInt("PlayerCarLevel", 0))
            {
                purchasedCarBuyButton[i].interactable = false;
            }
        }
    }


 
    public void BuyCar(int Carlvl)
    {
        int price = Carlvl* Carlvl * 5000;
        if (price < 10000)
            price = 10000;
        if (PlayerPrefs.GetInt("Money", 0) >= price)
        {
            int currentMoney = PlayerPrefs.GetInt("Money", 0) - price;
            PlayerPrefs.SetInt("Money", currentMoney);
            PlayerPrefs.SetInt("PlayerCarLevel", Carlvl);
            PurchasedCars();
            PlayerPrefs.SetInt("Upgrades", 0); // yeni araba alininca yukseltme sifir olur
        }
    }



    public void TweeningText()
    {
        textOfGameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
        {
            ClearText();
        });
    }

    public void ClearText()
    {
        textOfGameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
    }




}
