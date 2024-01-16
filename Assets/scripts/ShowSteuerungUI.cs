using UnityEngine;

public class ShowSteuerungUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public void ShowSteuerung(bool alpha)
    {
        canvasGroup.alpha = alpha ? 1 : 0;
    }
}
