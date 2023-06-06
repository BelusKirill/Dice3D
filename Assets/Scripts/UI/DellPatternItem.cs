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

    ItemSeatPattern actualSeat;
    bool backPressedOnce;
    IEnumerator coroutine;

    void Start()
    {
        delPatternCanvas.SetActive(false);
        backPressedOnce = false;
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), -350, 0.1f).setDelay(0.1f);
        
    }

    public void ShowMessage(ItemSeatPattern _actualSeat)
    {
        Debug.Log("ShowMessage");
        actualSeat = _actualSeat;
        backPressedOnce = true;
        delPatternCanvas.SetActive(true);

        ImageTimer.fillAmount = 0;
        txtTime.text = ((int)waitTime).ToString();
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), 0, 0.1f).setDelay(0.1f);

        coroutine = WaitCountdown();
        StartCoroutine(coroutine);
    }

    void HideMessage()
    {
        LeanTween.moveY(delPatternPanel.GetComponent<RectTransform>(), -350, 0.1f).setDelay(0.1f);
        backPressedOnce = false;
        Invoke("HideCanvas", 0.5f);
        StopCoroutine(coroutine);
    }

    void HideCanvas()
    {
        delPatternCanvas.SetActive(false);
    }

    IEnumerator WaitCountdown()
    {
        yield return null;

        var tempTime = 0f;
        while(tempTime <= waitTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else if (Input.touchCount > 0)
            {
                HideMessage();
            }

            tempTime += Time.deltaTime;
            ImageTimer.fillAmount = 1 - (tempTime * (waitTime / 10));
            txtTime.text = ((int)(waitTime - tempTime + 0.5)).ToString();
            yield return null;
        }
        HideMessage();
    }
}
