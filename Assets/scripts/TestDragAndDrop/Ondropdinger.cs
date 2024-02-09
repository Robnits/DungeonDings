using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ondropdinger : MonoBehaviour, IDropHandler
{
    public List<GameObject> list = new();

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            for(int i = 0; i < list.Count; i++)
            {
                //list[i].GetComponent<Drag>().GoToMiddle();
            }
        }
    }
}
