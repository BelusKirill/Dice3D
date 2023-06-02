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

    private ControllerDice controlDice;

    // Start is called before the first frame update
    void Start()
    {
        controlDice = controllerDice.GetComponent<ControllerDice>();
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
            ItemSeatPattern itemSeatPattern = activItem.GetComponent<ItemSeatPattern>();
            string msg = "";

            foreach (Dice dice in controlDice.dices)
            {
                msg += $"{dice.type}|{dice.typeTheam} ";
            }

            if (msg.Length < 2) return;

            itemSeatPattern.text = txtNamePattern.text;
            itemSeatPattern.pattern = msg;
        }
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
}
