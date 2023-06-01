using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patterns : MonoBehaviour
{
    public GameObject ContentPatten;
    public List<Transform> listPattern;
    public TextMeshProUGUI txtNamePattern;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in ContentPatten.transform)
        {
            listPattern.Add(child);
        }
    }

    public void BtnSave()
    {
        if (txtNamePattern.text.Trim().Length < 2)
            return; 

        Transform activItem = null;

        foreach(Transform item in listPattern)
        {
            if (item.GetComponent<Toggle>().isOn)
            {
                activItem = item;
            }
        }

        if (activItem != null)
        {
            activItem.GetComponent<ItemSeatPattern>().text = txtNamePattern.text;
        }
    }
}
