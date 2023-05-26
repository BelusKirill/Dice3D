using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class History : MonoBehaviour
{
    public List<ItemHistory> itemsHistory;
    public Transform 

    public void GetHistory()
    {
        DataTable dtHistory = MyDataBase.GetListHistory();

        foreach (DataRow row in dtHistory.Rows)
        {
            Debug.Log(row.ItemArray[0]);
            ItemHistory itemHistory = new ItemHistory();
            itemHistory.results = row.ItemArray[1].ToString();
            itemHistory.date = DateTime.Parse(row.ItemArray[0].ToString());
            itemsHistory.Add(itemHistory);
        }
    }
}
