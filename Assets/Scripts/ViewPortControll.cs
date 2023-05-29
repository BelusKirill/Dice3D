using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortControll : MonoBehaviour
{
    public int hideValue;

    void FixedUpdate()
    {
        //RectTransform yourRect = gameObject.GetComponent<RectTransform>();

        Vector2 yourRect = gameObject.GetComponent<RectTransform>().anchoredPosition;

        if (hideValue > yourRect.y)
            {
                Debug.Log("hide");
            }
    }
}
