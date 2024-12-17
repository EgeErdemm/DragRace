using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private int levelOfCar;
    [SerializeField] private TextMeshProUGUI engineLevelText;
    private Button[] upgradeButtons;
    private Image upgradesImage;
    int loseCount;
    bool glow;
    int currentLevelOfCar;
    int engineLevel;


    private void Start()
    {
        //PlayerPrefs.SetInt("Money", 20000);
        engineLevel = levelOfCar + 1;
        //PlayerPrefs.DeleteKey("Upgrades");
        // buying upgrades
        upgradeButtons = this.GetComponentsInChildren<Button>();
        upgradeButtons[0].onClick.AddListener( delegate {

            Upgrade(upgradeButtons[0]);
    	});
        upgradeButtons[1].onClick.AddListener(delegate {

            Upgrade(upgradeButtons[1]);
        });
        upgradeButtons[2].onClick.AddListener(delegate {

            Upgrade(upgradeButtons[2]);
        });

        //

        LockUpgrades();
        //
        //
        upgradesImage = transform.GetComponent<Image>();
        loseCount = PlayerPrefs.GetInt("LoseCount", 0);
        currentLevelOfCar = PlayerPrefs.GetInt("PlayerCarLevel", 0);
        if(currentLevelOfCar == levelOfCar) StartCoroutine(GlowUpgradeImg());

        //

        if(currentLevelOfCar == levelOfCar)
        {
            switch (PlayerPrefs.GetInt("Upgrades", 0))
            {
                case 0:
                    engineLevelText.text = "Engine Level: " + engineLevel.ToString();
                    break;
                case 1:
                    engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".15";
                    break;
                case 2:
                    engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".3";
                    break;
                case 3:
                    engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".5";
                    break;
                default:
                    engineLevelText.text = "Engine Level: " + engineLevel.ToString();
                    break;


            }

        }


    }


    private void Update()
    {
        if(loseCount >= 2 && currentLevelOfCar ==levelOfCar )
        {
            glow = true;

        }
        else
        {
            glow = false;
        }
    }


    public void Upgrade(Button button)
    {
        Debug.Log("Upgrade working");
        int upgradeLevel = PlayerPrefs.GetInt("Upgrades", 0);
        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        int penalty =0;
        int penaltyLevel = PlayerPrefs.GetInt("PlayerCarLevel", 0) +1;

        if (upgradeLevel == 0)
        {
            penalty = 1000 * penaltyLevel; 
        }
        else if (upgradeLevel == 1)
        {
            penalty = 2000 * penaltyLevel;
        }
        else if (upgradeLevel == 2)
        {
            penalty = 3000 * penaltyLevel;
        }

        if (currentMoney >= penalty & PlayerPrefs.GetInt("PlayerCarLevel", 0) >= levelOfCar)
        {
            currentMoney = PlayerPrefs.GetInt("Money", 0) - penalty;
            PlayerPrefs.SetInt("Money", currentMoney);
            upgradeLevel = PlayerPrefs.GetInt("Upgrades", 0) + 1;
            PlayerPrefs.SetInt("Upgrades", upgradeLevel);
            button.interactable = false;
        }



        switch (PlayerPrefs.GetInt("Upgrades", 0))
        {
            case 0:
                engineLevelText.text = "Engine Level: " + engineLevel.ToString();
                break;
            case 1:
                engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".15";
                break;
            case 2:
                engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".3";
                break;
            case 3:
                engineLevelText.text = "Engine Level: " + engineLevel.ToString() + ".5";
                break;
            default:
                engineLevelText.text = "Engine Level: " + engineLevel.ToString();
                break;


        }

    }

    public void LockUpgrades()
    {
        if(PlayerPrefs.GetInt("PlayerCarLevel", 0) > levelOfCar)
        {
            foreach(Button button in upgradeButtons)
            {
                button.interactable = false;
            }
        }
        else if(PlayerPrefs.GetInt("PlayerCarLevel", 0) == levelOfCar)
        {
            int upgradeLevel = PlayerPrefs.GetInt("Upgrades", 0);
         
            for(int i =0; i<upgradeLevel; i++)
            {
                upgradeButtons[i].interactable = false;
            }
        }


 

    }

    IEnumerator GlowUpgradeImg()
    {
        while (true)
        {
            if (glow && PlayerPrefs.GetInt("Upgrades", 0) != 3)
            {

                upgradesImage.color = Color.red;
                yield return new WaitForSeconds(1f);
                upgradesImage.color = Color.white;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return null;
            }
        }


    }






}
