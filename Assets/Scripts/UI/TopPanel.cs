using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopPanel : MonoBehaviour
{
    public TextMeshProUGUI tetResurl;
    public Animator animTopPanel;

    public void SetResult(int result)
    {
        tetResurl.text = result.ToString();
    }

    public void SetResult(string result, int[] results)
    {
        tetResurl.text = result;
        string strRes = String.Join("/", results);
        MyDataBase.InsertResult(strRes);
    }

    public void AnimClose()
    {
        animTopPanel.ResetTrigger("Open");
        animTopPanel.SetTrigger("Close");
    }

    public void AnimOpen()
    {
        animTopPanel.ResetTrigger("Close");
        animTopPanel.SetTrigger("Open");
    }
}
