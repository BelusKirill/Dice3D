using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Toggle is_anim;
    public Slider speed;
    public GameObject typeResOut;
    public GameObject selectThem;

    // Start is called before the first frame update
    void Start()
    {
        MyDataBase.CheckedDate();

        is_anim.isOn = MyDataBase.GetParamSetting("is_anim") != "f";
        speed.value = float.Parse(MyDataBase.GetParamSetting("speed"));
        typeResOut.GetComponent<ToggleGroup>().SetIdToggleIsON(int.Parse(MyDataBase.GetParamSetting("type_res_out")));
        selectThem.GetComponent<ToggleGroup>().SetIdToggleIsON(int.Parse(MyDataBase.GetParamSetting("selected_them")));
    }

    public void SetIsAnim(Toggle toggle)
    {
        MyDataBase.SetParamBoolSetting("is_anim", toggle.isOn);
    }

    public void SetSpeed(Slider slider)
    {
        MyDataBase.SetParamIntSetting("speed", (int)slider.value);
    }

    public void SetIdToggleIsON()
    {
        MyDataBase.SetParamIntSetting("type_res_out", typeResOut.GetComponent<ToggleGroup>().GetIdToggleIsON() - 1);
    }

    public void SetIdSelectThem()
    {
        MyDataBase.SetParamIntSetting("selected_them", selectThem.GetComponent<ToggleGroup>().GetIdToggleIsON() - 1);
    }
}
