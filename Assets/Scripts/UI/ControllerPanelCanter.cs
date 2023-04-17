using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPanelCanter : MonoBehaviour
{
    [Header("Анимация вернего бара")]
    public Animator anim;

    private bool _states = true;

    public void ChangingState()
    {
        if (_states)
        {
            anim.SetTrigger("Open");
            _states = !_states;
        }
        else
        {
            anim.SetTrigger("Close");
            _states = !_states;
        }
    }

    public void Test()
    {
        Debug.Log("TTT");
    }
}
