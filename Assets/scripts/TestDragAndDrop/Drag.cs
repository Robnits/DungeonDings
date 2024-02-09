using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Dynamic;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private UnityEngine.UI.Image image;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

}
