using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public event Action<Vector2> Drag;
    public event Action<Vector2> EndTouch;
    public event Action<Vector2> BeginTouch;
    public event Action Touch;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
            return;
        BeginTouch?.Invoke(eventData.position);
        // if (eventData.pointerId != 0)
        //     return;
        // BeginTouch?.Invoke(eventData.pressPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
            return;
        Drag?.Invoke(eventData.position);
        // if (eventData.pointerId != 0)
        //     return;
        // Drag?.Invoke(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndTouch?.Invoke(eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Touch?.Invoke();
    }
}