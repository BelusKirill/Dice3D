using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPanelCanter : MonoBehaviour
{
    [Header("Анимация вернего бара")]
    public Animator animPanelDices;
    public Animator animPanelSetting;
    public Animator animPanelThemes;

    private bool _DicesStates = true;
    private bool _SettingStates = true;
    private bool _ThemesStates = true;

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
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelDices);
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
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelSetting);
            _SettingStates = !_SettingStates;
        }
        else
        {
            Setting();
        }
    }

    public void PressThemesBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelThemes);
            _ThemesStates = !_ThemesStates;
        }
        else if (!_SettingStates)
        {
            SwipeSetting(animPanelThemes);
            _ThemesStates = !_ThemesStates;
        }
        else
        {
            Themes();
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

    private void Themes()
    {
        if (_ThemesStates)
        {
            animPanelThemes.SetTrigger("Open");
            _ThemesStates = !_ThemesStates;
        }
        else
        {
            animPanelThemes.SetTrigger("Close");
            _ThemesStates = !_ThemesStates;
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

    private void SwipeThemes(Animator anim)
    {
        animPanelThemes.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        _ThemesStates = !_ThemesStates;
    }
}
