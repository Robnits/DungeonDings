using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDropdownScroller : MonoBehaviour, ISelectHandler
{
    private ScrollRect scrollRect;
    private float scrollPosition = 1;

    void Start()
    {
        scrollRect = GetComponentInParent<ScrollRect>(true);

        if (scrollRect != null)
        {
            int childCount = scrollRect.content.transform.childCount;
            int childIndex = transform.GetSiblingIndex();

            float stepSize = 1.0f / (childCount - 1);
            scrollPosition = 1 - (childIndex * stepSize);
            
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = Mathf.Max(0, scrollPosition - 0.1f);
        }
    }
}
