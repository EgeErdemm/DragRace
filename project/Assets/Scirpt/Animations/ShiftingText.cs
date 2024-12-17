using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShiftingText : MonoBehaviour
{

    public static ShiftingText Instance;

    [SerializeField] private TextMeshProUGUI shiftingCaseText;
    [SerializeField] private TextMeshProUGUI shiftingBonusText;
    private GameObject Casetxt;
    [SerializeField] private Image AlertPanel;
    private Tween redTween;


    public string hexRed, hexGreen, hexOrange, hexGrey;


    private void Awake()
    {
        if(Instance !=null & Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Casetxt = gameObject;

    }

    public void KillTweensRed()
    {
        if(redTween !=null)
        {
            redTween.Kill();
            Debug.Log("Killed tween");
            Color currentColor = AlertPanel.color;
            AlertPanel.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
            redTween = null;


        }
        else
        {
            Debug.Log("There is no tween");
        }
    }

    public void RedTweenStart()
    {
        if (redTween != null) return;
        Color originalColor = AlertPanel.color;
        Color fullColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
        redTween = AlertPanel.DOColor(fullColor, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .From(originalColor);
    }

    public void RedShiftingText()
    {
        if (ColorUtility.TryParseHtmlString(hexRed, out Color newColor))
        {
            Casetxt.transform.localScale = new Vector3(1f, 1f, 1f);
            shiftingCaseText.color = newColor;
            shiftingBonusText.color = newColor;
            shiftingCaseText.text = "Shifting Needed";
            shiftingBonusText.text = "";
            //
  
         

        }
        else
        {
            Debug.LogError("Hex kodu geçersiz!");
        }
    }

    public void GreenShiftingText()
    {
        if (ColorUtility.TryParseHtmlString(hexGreen, out Color newColor))
        {
            shiftingCaseText.color = newColor;
            shiftingBonusText.color = newColor;
            shiftingCaseText.text = "Perfect Shifting";
            shiftingBonusText.text = "Velocity Bonus +10";
            TweeningTextScale();

        }
        else
        {
            Debug.LogError("Hex kodu geçersiz!");
        }
    }

    public void OrangeShiftingText()
    {
        if (ColorUtility.TryParseHtmlString(hexOrange, out Color newColor))
        {
            shiftingCaseText.color = newColor;
            shiftingBonusText.color = newColor;
            shiftingCaseText.text = "Good Shifting";
            shiftingBonusText.text = "Velocity Bonus +5";
            TweeningTextScale();

        }
        else
        {
            Debug.LogError("Hex kodu geçersiz!");
        }
    }

    public void GreyShiftingText()
    {
        if (ColorUtility.TryParseHtmlString(hexGrey, out Color newColor))
        {
            shiftingCaseText.color = newColor;
            shiftingBonusText.color = newColor;
            shiftingCaseText.text = "Bad Shifting";
            shiftingBonusText.text = "";
            TweeningTextScale();

        }
        else
        {
            Debug.LogError("Hex kodu geçersiz!");
        }
    }

    public void ClearShiftingText()
    {
        shiftingCaseText.text = "";
        shiftingBonusText.text = "";
        Casetxt.transform.localScale = new Vector3(0f, 0f, 0f);

    }


    private void TweeningTextScale()
    {

        Casetxt.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
        {
            ClearShiftingText();
        });

    }


}
