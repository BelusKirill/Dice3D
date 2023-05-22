using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPanelCanter : MonoBehaviour
{
    [Header("Анимация вернего бара")]
    public Animator animPanelDices;
    public Animator animPanelSetting;
    public Animator animPanelThemes;
    public Animator animPanelPattern;
    public Animator animPanelHistory;

    public Button btnDices;
    public Button btnSetting;
    public Button btnThemes;
    public Button btnPattern;
    public Button btnHistory;

    public Color normalColor;
    public Color selectedColor;

    private bool _DicesStates = true;
    private bool _SettingStates = true;
    private bool _ThemesStates = true;
    private bool _PatternStates = true;
    private bool _HistoryStates = true;

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

// 1
    public void PressDicesBtn()
    {
        if (!_SettingStates)
        {
            SwipeSetting(animPanelDices);
        }
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelDices);
        }
        else if (!_PatternStates)
        {
            SwipePattern(animPanelDices);
        }
        else if (!_HistoryStates)
        {
            SwipeHistory(animPanelDices);
        }
        else
        {
            Dices();
            return;
        }
        _DicesStates = !_DicesStates;
    }

    public void PressSettingBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelSetting);
        }
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelSetting);
        }
        else if (!_PatternStates)
        {
            SwipePattern(animPanelSetting);
        }
        else if (!_HistoryStates)
        {
            SwipeHistory(animPanelSetting);
        }
        else
        {
            Setting();
            return;
        }
        SetColorSelected(btnSetting);
        _SettingStates = !_SettingStates;

    }

    public void PressThemesBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelThemes);
        }
        else if (!_SettingStates)
        {
            SwipeSetting(animPanelThemes);
        }
        else if (!_PatternStates)
        {
            SwipePattern(animPanelThemes);
        }
        else if (!_HistoryStates)
        {
            SwipeHistory(animPanelThemes);
        }
        else
        {
            Themes();
            return;
        }
        SetColorSelected(btnThemes);
        _ThemesStates = !_ThemesStates;
    }

    public void PressPatternBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelPattern);
        }
        else if (!_SettingStates)
        {
            SwipeSetting(animPanelPattern);
        }
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelPattern);
        }
        else if (!_HistoryStates)
        {
            SwipeHistory(animPanelPattern);
        }
        else
        {
            Pattern();
            return;
        }
        SetColorSelected(btnPattern);
        _PatternStates = !_PatternStates;
    }

    public void PressHistoryBtn()
    {
        if (!_DicesStates)
        {
            SwipeDice(animPanelHistory);
        }
        else if (!_SettingStates)
        {
            SwipeSetting(animPanelHistory);
        }
        else if (!_ThemesStates)
        {
            SwipeThemes(animPanelHistory);
        }
        else if (!_PatternStates)
        {
            SwipePattern(animPanelHistory);
        }
        else
        {
            History();
            return;
        }
        SetColorSelected(btnHistory);
        _HistoryStates = !_HistoryStates;
    }

// 2
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
            SetColorSelected(btnSetting);
            _SettingStates = !_SettingStates;
        }
        else
        {
            animPanelSetting.SetTrigger("Close");
            SetColorNormal(btnSetting);
            _SettingStates = !_SettingStates;
        }
    }

    private void Themes()
    {
        if (_ThemesStates)
        {
            animPanelThemes.SetTrigger("Open");
            SetColorSelected(btnThemes);
            _ThemesStates = !_ThemesStates;
        }
        else
        {
            animPanelThemes.SetTrigger("Close");
            SetColorNormal(btnThemes);
            _ThemesStates = !_ThemesStates;
        }
    }

    private void Pattern()
    {
        if (_PatternStates)
        {
            animPanelPattern.SetTrigger("Open");
            SetColorSelected(btnPattern);
            _PatternStates = !_PatternStates;
        }
        else
        {
            animPanelPattern.SetTrigger("Close");
            SetColorNormal(btnPattern);
            _PatternStates = !_PatternStates;
        }
    }

    private void History()
    {
        if (_HistoryStates)
        {
            animPanelHistory.SetTrigger("Open");
            SetColorSelected(btnHistory);
            _HistoryStates = !_HistoryStates;
        }
        else
        {
            animPanelHistory.SetTrigger("Close");
            SetColorNormal(btnHistory);
            _HistoryStates = !_HistoryStates;
        }
    }

// 3
    private void SwipeSetting(Animator anim)
    {
        animPanelSetting.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        SetColorNormal(btnSetting);
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
        SetColorNormal(btnThemes);
        _ThemesStates = !_ThemesStates;
    }

    private void SwipePattern(Animator anim)
    {
        animPanelPattern.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        SetColorNormal(btnPattern);
        _PatternStates = !_PatternStates;
    }

    private void SwipeHistory(Animator anim)
    {
        animPanelHistory.SetTrigger("Swipe1");
        anim.SetTrigger("Swipe2");
        SetColorNormal(btnHistory);
        _HistoryStates = !_HistoryStates;
    }

// 4
    private void SetColorSelected(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = selectedColor;
        cb.selectedColor = selectedColor;
        button.colors = cb;
    }

    private void SetColorNormal(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = normalColor;
        cb.selectedColor = normalColor;
        button.colors = cb;
    }
}
