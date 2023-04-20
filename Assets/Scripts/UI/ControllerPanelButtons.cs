using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPanelButtons : MonoBehaviour
{
    public Button[] buttons;
    public ControllerDice controllerDice;

    void Start()
    {
        StartCoroutine(InteractableButtons());
    }

    IEnumerator InteractableButtons()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);  

            if (controllerDice.GetComponent<ControllerDice>().IsRunSping() && controllerDice.GetComponent<ControllerDice>().dices.Count != 0)
            {
                foreach (Button button in buttons)
                {
                    button.interactable = false;
                }
            }
            else
            {
                foreach (Button button in buttons)
                {
                    button.interactable = true;
                }
            }    
        }
    }
}
