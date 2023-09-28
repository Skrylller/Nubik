using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TouchPanel : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{

    public UnityEvent OnTouch;
    public UnityEvent OnClick;

    
    public void OnPointerClick(PointerEventData data)
    {
        OnClick?.Invoke();
    }

    public void OnPointerMove(PointerEventData data)
    {
        OnTouch?.Invoke();
    }
}
