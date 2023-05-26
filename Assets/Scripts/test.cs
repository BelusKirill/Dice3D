using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    public TextMeshProUGUI txtField;

    void Start()
    {
        // Важно !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Создает отсутствующие таблицы
        MyDataBase.CreateTables();

        string version = MyDataBase.ExecuteQueryWithAnswer("select sqlite_version()");
        DataTable fables = MyDataBase.GetTable("SELECT name FROM sqlite_master WHERE type='table' and name not LIKE '%sequence' and name not LIKE '%old%'");
        string fablesStr = "";

        foreach (DataRow row in fables.Rows)
        {
            fablesStr += row.ItemArray[0] + " ";
        }

        txtField.text = "Версия sqlite: " + version + "\n" + "Таблмцы: " + fablesStr;
    }

    public void UpdateTxt(string txt)
    {
        txtField.text = txt;
    }
}
