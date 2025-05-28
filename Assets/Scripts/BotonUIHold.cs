using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BotonUIHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Action onDown;
    private Action onUp;

    public void Init(Action down, Action up)
    {
        onDown = down;
        onUp = up;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke();
    }
}
