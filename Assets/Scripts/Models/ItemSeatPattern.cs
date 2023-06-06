using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSeatPattern : MonoBehaviour
{
    public GameObject dellPatternItem;
    public TextMeshProUGUI txtName;
    public GameObject btnPanel;
    public string pattern;
    public int numPosition;

    public string text 
    {
        get
        {
            return txtName.text;
        }

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
        MyDataBase.RemovePattern(numPosition);
        dellPatternItem.GetComponent<DellPatternItem>().ShowMessage(this);
    }

    public void UpLoad()
    {
        Debug.Log("gg");
    }
}
