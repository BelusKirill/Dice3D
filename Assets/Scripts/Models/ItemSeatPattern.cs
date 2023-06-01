using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSeatPattern : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public GameObject btnPanel;
    public string pattern;

    public string text 
    {
        set
        {
            txtName.text = value;
            btnPanel.SetActive(true);
        }
    }

    public void btnDell()
    {
        pattern = "";
        txtName.text = "< Пусто >";
        btnPanel.SetActive(false);
    }

    public void UpLoad()
    {
        Debug.Log("gg");
    }
}
