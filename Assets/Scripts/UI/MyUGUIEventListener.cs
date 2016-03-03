// 相关事件有需要再加
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MyUGUIEventListener : EventTrigger
{
    public delegate void VoidDelegate(GameObject obj);
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;

    static public MyUGUIEventListener Get(GameObject obj)
    {
        MyUGUIEventListener listener = obj.GetComponent<MyUGUIEventListener>();
        if (listener == null) listener = obj.AddComponent<MyUGUIEventListener>();
        return listener;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
}
