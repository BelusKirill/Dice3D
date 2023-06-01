using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollItem : MonoBehaviour
{
    public string diceType;
    public GameObject controllerDice;
    public TextMeshProUGUI txtCountDices;
    public int countDices;

    private void Start() 
    {
        countDices = 0;
        txtCountDices.text = countDices.ToString();
    }

    public void AddDice()
    {
        if (countDices < 12 && controllerDice.GetComponent<ControllerDice>().dices.Count < 12)
        {
            countDices++;
            txtCountDices.text = countDices.ToString();
            Transform newDiceT = Instantiate(controllerDice.GetComponent<Theams>().GetDice(diceType));
            int typeTheam = controllerDice.GetComponent<Theams>().GetTypeTheam();
            newDiceT.SetParent(controllerDice.transform);
            Dice newDice = new Dice();
            newDice.transform = newDiceT;
            newDice.typeTheam = typeTheam;
            newDice.type = diceType;
            controllerDice.GetComponent<ControllerDice>().AddDice(newDice);
        }
    }

    public void DelDice()
    {
        if (countDices > 0)
        {
            countDices--;
            controllerDice.GetComponent<ControllerDice>().DelDice(diceType);
            txtCountDices.text = countDices.ToString();
        }
    }

    public void ClearDices()
    {
        countDices = 0;
        txtCountDices.text = countDices.ToString();
    }
}
