using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSeatPattern : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public GameObject btnPanel;

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
        txtName.text = "< Пусто >";
        btnPanel.SetActive(false);
    }
}
