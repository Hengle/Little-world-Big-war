using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CatchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager._instance.CatchState = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager._instance.CatchState = false;
    }
}
