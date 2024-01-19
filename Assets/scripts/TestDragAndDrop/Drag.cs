using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Dynamic;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UnityEngine.UI.Image image;

    private Vector3 startposition;
    private Vector3 endposition;

    private void Start()
    {
        startposition = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.position.x > 370 && transform.position.y < 420 &&
            transform.position.y > 200 && transform.position.y < 250) 
        {
            
        }
        else
        {
            print("test");
            transform.position = startposition;
        }
    }

    private void GoToMiddle() { }
}
