using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ItemHistoryPf : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtResult;

    public string date 
    {
        set
        {
            txtDate.text = value;
        }
    }

    public string result 
    {
        set
        {
            txtResult.text = value;
        }
    }
}