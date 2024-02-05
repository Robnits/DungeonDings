using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Dynamic;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UnityEngine.UI.Image image;

    private Vector3 startposition;
    private Vector3 endposition = new Vector3(Screen.width / 2,Screen.height / 2,0);

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
<<<<<<< Updated upstream
        
    }

=======
        //transform.position = startposition;
    }

    public void GoToMiddle()
    {

        transform.position = endposition;
    }
>>>>>>> Stashed changes
}
