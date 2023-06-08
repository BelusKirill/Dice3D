using System.Data;
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
    public TMP_InputField txtInputNamePattern;
    public GameObject controllerDice;
    public Alert alert;

    [Header("ToggleGroupPattern")]
    public GameObject tgp;

    [Header("Buttons")]
    public GameObject btnSave;
    public GameObject btnUpload;

    private ControllerDice controlDice;

    // Start is called before the first frame update
    void Start()
    {
        DataTable dbPattern = MyDataBase.GetTable("SELECT * FROM \"pattern\"");

        controlDice = controllerDice.GetComponent<ControllerDice>();
        foreach(Transform child in ContentPatten.transform)
        {
            foreach (DataRow row in dbPattern.Rows)
            {
                ItemSeatPattern itemSeatPattern = child.GetComponent<ItemSeatPattern>();
                if (itemSeatPattern.numPosition == int.Parse(row.ItemArray[1].ToString()))
                {
                    itemSeatPattern.pattern = row.ItemArray[3].ToString();
                    itemSeatPattern.text = row.ItemArray[2].ToString();
                }
            }

            listPattern.Add(child);
        }

        LeanTween.scaleX(btnUpload, 0, 0.1f).setDelay(0.1f);
        LeanTween.moveX(btnSave.GetComponent<RectTransform>(), -60, 0.1f).setDelay(0.1f);
    }

    public void BtnSave()
    {
        if (txtNamePattern.text.Trim().Length < 2)
        {
            alert.SetText("Не указано название");
            alert.ShowMessage();
            return; 
        }

        Transform activItem = null;
        int numActivItem = 0;

        foreach(Transform item in listPattern)
        {
            if (item.GetComponent<Toggle>().isOn)
            {
                activItem = item;
                break;
            }
            numActivItem ++;
        }

        if (activItem != null)
        {
            ItemSeatPattern itemSeatPattern = activItem.GetComponent<ItemSeatPattern>();
            string msg = "";

            foreach (Dice dice in controlDice.dices)
            {
                msg += $"{dice.type}|{dice.typeTheam} ";
            }

            if (msg.Length < 2)
            {
                alert.SetText("Поле с кубиками пустое, нечего сохранять");
                alert.ShowMessage();
                return;
            }

            itemSeatPattern.text = txtNamePattern.text;
            itemSeatPattern.pattern = msg;
            MyDataBase.InsertPattern(numActivItem, itemSeatPattern.text, itemSeatPattern.pattern);
        }
        else
        {
            alert.SetText("Не выбрана ячейка для сохранения");
            alert.ShowMessage();
        }

        UpdateBtns();
    }

    public void Upload()
    {
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
            ItemSeatPattern itemSeatPattern = activItem.GetComponent<ItemSeatPattern>();
            
            if (itemSeatPattern.pattern.Length > 1)
            {
                txtInputNamePattern.text = itemSeatPattern.text;

                controlDice.DelDices();
                foreach (string item in itemSeatPattern.pattern.Trim().Split(" "))
                {
                    string diceType = item.Split("|")[0];
                    int typeTheam = int.Parse(item.Split("|")[1]);

                    Transform newDiceT = Instantiate(controllerDice.GetComponent<Theams>().GetDice(diceType, typeTheam));

                    Dice newDice = new Dice();
                    newDice.transform = newDiceT;
                    newDice.typeTheam = typeTheam;
                    newDice.type = diceType;

                    
                    controlDice.AddDice(newDice);
                }
            }
        }
    }

    public void UpdateBtns()
    {
        GameObject itemPattern = tgp.GetComponent<ToggleGroup>().GetGOToggleIsON();
        if (itemPattern != null && itemPattern.GetComponent<Toggle>().isOn)
        {
            if (itemPattern.GetComponent<ItemSeatPattern>().pattern.Length > 1)
            {
                btnUpload.SetActive(true);
                LeanTween.scaleX(btnUpload, 1, 0.1f).setDelay(0.1f);
                LeanTween.moveX(btnSave.GetComponent<RectTransform>(), 0, 0.1f).setDelay(0.1f);
            }
            else
            {
                btnUpload.SetActive(false);
                LeanTween.scaleX(btnUpload, 0, 0.1f).setDelay(0.1f);
                LeanTween.moveX(btnSave.GetComponent<RectTransform>(), -60, 0.1f).setDelay(0.1f);
            }
        }
    }
}
