using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPanelCanter : MonoBehaviour
{
    [Header("Анимация вернего бара")]
    public Animator animPanelDices;
    public Animator animPanelSetting;

    private bool _DicesStates = true;
    private bool _SettingStates = true;

    public void ChangingState()
    {
        if (_DicesStates)
        {
            animPanelDices.SetTrigger("Open");
            _DicesStates = !_DicesStates;
        }
        else
        {
            animPanelDices.SetTrigger("Close");
            _DicesStates = !_DicesStates;
        }
    }

    public void PressDicesBtn()
    {
        if (!_SettingStates)
        {
            SwipeSetting(animPanelDices);
            _DicesStates = !_DicesStates;
        }
        else
        {
            Dices();
        }
    }

    public void PressSettingBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelSetting);
            _SettingStates = !_SettingStates;
        }
        else
        {
            Setting();
        }
    }

    private void Dices()
    {
        if (_DicesStates)
        {
            animPanelDices.SetTrigger("Open");
            _DicesStates = !_DicesStates;
        }
        else
        {
            animPanelDices.SetTrigger("Close");
            _DicesStates = !_DicesStates;
        }
    }

    private void Setting()
    {
        if (_SettingStates)
        {
            animPanelSetting.SetTrigger("Open");
            _SettingStates = !_SettingStates;
        }
        else
        {
            animPanelSetting.SetTrigger("Close");
            _SettingStates = !_SettingStates;
        }
    }

    private void SwipeSetting(Animator anim)
    {
        animPanelSetting.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        _SettingStates = !_SettingStates;
    }

    private void SwipeDice(Animator anim)
    {
        animPanelDices.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        _DicesStates = !_DicesStates;
    }
}
