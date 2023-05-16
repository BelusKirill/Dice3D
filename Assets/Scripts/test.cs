using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    public TextMeshProUGUI txtField;

    // Start is called before the first frame update
    void Start()
    {
        string version = MyDataBase.ExecuteQueryWithAnswer("select sqlite_version()");
        string fables = MyDataBase.ExecuteQueryWithAnswer("SELECT name FROM sqlite_master WHERE type='table'");
        txtField.text = "Версия sqlite: " + version + "\n" + "Таблмцы: " + fables;
    }
}
