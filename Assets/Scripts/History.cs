using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class History : MonoBehaviour
{
    public List<ItemHistory> itemsHistory;
    public List<Transform> itemList;
    public GameObject itemPrefab;
    public Transform spawn;

    public void GetHistory()
    {
        DataTable dtHistory = MyDataBase.GetListHistory();
        itemsHistory = new List<ItemHistory>();

        foreach (DataRow row in dtHistory.Rows)
        {
            ItemHistory itemHistory = new ItemHistory();
            itemHistory.results = row.ItemArray[1].ToString();
            itemHistory.date = DateTime.Parse(row.ItemArray[0].ToString());
            itemsHistory.Add(itemHistory);
        }

        UpdateHistoryUI();
    }

    private void UpdateHistoryUI()
    {
        ClearHistoryUI();

        foreach (ItemHistory itemHistory in itemsHistory)
        {
            Transform item = Instantiate(itemPrefab.transform);
            string[] results = itemHistory.results.Split("/");
            string result = "";
            int sum = 0;

            foreach (string _ in results)
            {
                result += _ + " ";
                sum += Int32.Parse(_);
            }

            result += $"({sum})";

            item.gameObject.GetComponent<ItemHistoryPf>().date = itemHistory.date.ToString();
            item.gameObject.GetComponent<ItemHistoryPf>().result = result;
            item.SetParent(spawn);
            itemList.Add(item);
        }
    }

    private void ClearHistoryUI()
    {
        foreach (Transform item in itemList)
        {
            Destroy(item.gameObject);
        }
        itemList = new List<Transform>();
    }
}
