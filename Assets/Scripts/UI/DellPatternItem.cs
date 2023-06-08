using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DellPatternItem : MonoBehaviour
{
    [SerializeField] GameObject delPatternCanvas;
    [SerializeField] GameObject delPatternPanel;
    [SerializeField] float waitTime;
    [SerializeField] Image ImageTimer;
    [SerializeField] TextMeshProUGUI txtTime;

    [Header("PatternPanel")]
    public GameObject patternPanel;

    ItemSeatPattern actualSeat;
    string actualText;
    string actualTxtPattern;
    bool backPressedOnce;
    IEnumerator coroutine;

    void Start()
    {
        delPatternCanvas.SetActive(false);
        backPressedOnce = false;
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), -350, 0.1f).setDelay(0.1f);
        actualSeat = null;
    }

    public void updateActualSeat(ItemSeatPattern _actualSeat)
    {
        actualSeat = _actualSeat;
        actualText = _actualSeat.text;
        actualTxtPattern = _actualSeat.pattern;
        _actualSeat.Remove();
    }

    public void ShowMessage(ItemSeatPattern _actualSeat)
    {
        if (backPressedOnce)
        {
            HideMessage(false);
            HideCanvas();
        }

        updateActualSeat(_actualSeat);
        backPressedOnce = true;
        delPatternCanvas.SetActive(true);

        ImageTimer.fillAmount = 0;
        txtTime.text = ((int)waitTime).ToString();
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), 0, 0.1f).setDelay(0.1f);

        coroutine = WaitCountdown();
        StartCoroutine(coroutine);
    }

    void HideMessage(bool hide = true, bool removeDB = true)
    {
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), -350, 0.1f).setDelay(0.1f);
        if (hide)
            Invoke("HideCanvas", 0.5f);
        if (removeDB)
            actualSeat.RemoveDB();
        StopCoroutine(coroutine);
    }

    void HideCanvas()
    {
        delPatternCanvas.SetActive(false);
        backPressedOnce = false;
    }

    IEnumerator WaitCountdown()
    {
        yield return null;

        var tempTime = 0f;
        while(tempTime <= waitTime)
        {
            tempTime += Time.deltaTime;
            ImageTimer.fillAmount = 1 - (tempTime / waitTime);
            txtTime.text = ((int)(waitTime - tempTime + 0.5)).ToString();
            yield return null;
        }
        HideMessage();
    }

    public void BtnPressCansel()
    {
        if (actualSeat == null) return;

        actualSeat.text = actualText;
        actualSeat.pattern = actualTxtPattern;
        HideMessage(removeDB: false);
        patternPanel.GetComponent<Patterns>().UpdateBtns();
    }
}
