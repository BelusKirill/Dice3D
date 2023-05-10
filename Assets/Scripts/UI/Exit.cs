using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject panelExit;

    private void Start() 
    {
        panelExit.SetActive(false);
    }

    public void ShowExitPanel()
    {
        panelExit.SetActive(true);
    }

    public void CloseExitPanel()
    {
        panelExit.SetActive(false);
    }

    public void AppExit()
    {
        Application.Quit();
    }
}
