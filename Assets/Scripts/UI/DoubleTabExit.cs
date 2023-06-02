using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTabExit : MonoBehaviour
{
    [SerializeField] GameObject exitCanvas;
    [SerializeField] GameObject exitPanel;
    [SerializeField] float waitTime;

    bool backPressedOnce;
    IEnumerator coroutine;

    void Start()
    {
        exitCanvas.SetActive(false);
        backPressedOnce = false;
        LeanTween.moveY(exitPanel.GetComponent<RectTransform>(), -300, 0.1f).setDelay(0.1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !backPressedOnce)
        {
            ShowMessage();
        }
    }

    void ShowMessage()
    {
        Debug.Log("ShowMessage");
        backPressedOnce = true;
        exitCanvas.SetActive(true);

        LeanTween.moveY(exitPanel.GetComponent<RectTransform>(), 100, 0.1f).setDelay(0.1f);

        coroutine = WaitCountdown();
        StartCoroutine(coroutine);
    }

    void HideMessage()
    {
        Debug.Log("HideMessage");
        LeanTween.moveY(exitPanel.GetComponent<RectTransform>(), -300, 0.1f).setDelay(0.1f);
        backPressedOnce = false;
        Invoke("HideCanvas", 0.5f);
        StopCoroutine(coroutine);
    }

    void HideCanvas()
    {
        exitCanvas.SetActive(false);
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
            yield return null;
        }
        HideMessage();
    }
}
