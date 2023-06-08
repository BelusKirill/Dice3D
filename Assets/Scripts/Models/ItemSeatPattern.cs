using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSeatPattern : MonoBehaviour
{
    public GameObject dellPatternItem;
    public GameObject panelPattern;
    public TextMeshProUGUI txtName;
    public GameObject btnPanel;
    public Image selectedImage;
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
            LeanColorSelect(new Color32(1, 222, 154, 100));
        }
    }

    public void LeanColorSelect(Color toColor)
    {
        Color fromColor = new Color32(1, 222, 154, 47);

        LeanTween.value(selectedImage.gameObject, setColorCallback, fromColor, toColor, .25f);
        LeanTween.value(selectedImage.gameObject, setColorCallback, toColor, fromColor, .25f);
    }

    private void setColorCallback( Color c )
    {
        selectedImage.color = c;
    }

    public void btnDell()
    {
        LeanColorSelect(new Color32(222, 50, 1, 100));
        dellPatternItem.GetComponent<DellPatternItem>().ShowMessage(this);
        panelPattern.GetComponent<Patterns>().UpdateBtns();
    }

    public void Remove()
    {
        pattern = "";
        txtName.text = "< Пусто >";
        btnPanel.SetActive(false);
    }

    public void RemoveDB()
    {
        MyDataBase.RemovePattern(numPosition);
    }

    public void UpLoad()
    {
        Debug.Log("gg");
    }
}
