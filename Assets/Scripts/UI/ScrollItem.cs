using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollItem : MonoBehaviour
{
    public Transform dice;
    public string diceType;
    public GameObject controllerDice;
    public TextMeshProUGUI txtCountDices;
    public int countDices;
    public Button leftBtn;
    public Button rightBtn;

    private void Start() 
    {
        countDices = 0;
        txtCountDices.text = countDices.ToString();
        StartCoroutine(InteractableButtons());
    }

    IEnumerator InteractableButtons()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);  

            if (controllerDice.GetComponent<ControllerDice>().IsRunSping() && controllerDice.GetComponent<ControllerDice>().dices.Count != 0)
            {
                leftBtn.interactable = false;
                rightBtn.interactable = false;
            }
            else
            {
                leftBtn.interactable = true;
                rightBtn.interactable = true;
            }    
        }
    }

    public void AddDice()
    {
        if (countDices < 12)
        {
            countDices++;
            txtCountDices.text = countDices.ToString();
            Transform newDiceT = Instantiate(dice);
            newDiceT.SetParent(controllerDice.transform);
            Dice newDice = new Dice();
            newDice.transform = newDiceT;
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
}
