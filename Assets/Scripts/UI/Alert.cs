using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alert : MonoBehaviour
{
    [SerializeField] GameObject alertCanvas;
    [SerializeField] GameObject alertPanel;
    [SerializeField] float waitTime;
    [SerializeField] TextMeshProUGUI alertText;
    
    bool backPressedOnce;
    IEnumerator coroutine;

    public void SetText(string text)
    {
        alertText.text = text;
    }

    void Start()
    {
        LeanTween.scale(alertPanel, new Vector2(0f,0f), 0.05f).setDelay(0.1f);
        alertCanvas.SetActive(false);
        backPressedOnce = false;
    }   

    public void ShowMessage()
    {
        backPressedOnce = true;
        alertCanvas.SetActive(true);

        LeanTween.scale(alertPanel, new Vector2(1,1), 0.05f).setDelay(0.1f);

        coroutine = WaitCountdown();
        StartCoroutine(coroutine);
    }

    void HideMessage()
    {
        LeanTween.scale(alertPanel, new Vector2(0f,0f), 0.05f);
        backPressedOnce = false;
        Invoke("HideCanvas", 0.1f);
        StopCoroutine(coroutine);
    }

    void HideCanvas()
    {
        alertCanvas.SetActive(false);
    }

        IEnumerator WaitCountdown()
    {
        yield return null;

        var tempTime = 0f;
        while(tempTime <= waitTime)
        {
            if (Input.touchCount > 0)
            {
                HideMessage();
            }

            tempTime += Time.deltaTime;
            yield return null;
        }
        HideMessage();
    }
}
