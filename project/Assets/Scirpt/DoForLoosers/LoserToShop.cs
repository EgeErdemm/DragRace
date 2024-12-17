using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoserToShop : MonoBehaviour
{

    public static LoserToShop Instance;

    [SerializeField] private Button ShopButton;

    private bool glow = false;

    int loseCount;

 

    private void Start()
    {
        loseCount = PlayerPrefs.GetInt("LoseCount", 0);

        if (loseCount >= 2)
        {
            StartShopButtonGlow();
        }
    }

    public void StartShopButtonGlow()
    {
        glow = true;
        StartCoroutine(GlowEffect(ShopButton));
    }


    public void StopGlowing()
    {
        glow = false;
    }
    

    IEnumerator GlowEffect(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Color originalColor = buttonImage.color;

        while (true)
        {

            if (glow)
            {
                buttonImage.color = button.colors.pressedColor;
                rectTransform.localScale = new Vector3(1.01f, 1.01f, 1.01f);

                yield return new WaitForSeconds(0.3f);

                buttonImage.color = originalColor;
                rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);



                yield return new WaitForSeconds(0.3f);

                Debug.Log("glow");
            }
            else
            {
                yield return null;
            }

        }




    }

}
