using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroup : MonoBehaviour
{
    public Toggle[] toggles;

    public int GetIdToggleIsON()
    {
        int res = 0;
        foreach (Toggle toggle in toggles)
        {
            res++;
            if (toggle.isOn)
                break;
        }

        return res;
    }

    public void SetIdToggleIsON(int id)
    {
        if (id < toggles.Length)
            toggles[id].isOn = true;
    }
}
