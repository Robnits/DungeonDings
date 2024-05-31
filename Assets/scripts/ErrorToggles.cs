using UnityEngine;
using UnityEngine.UI;

public class ToggleProblem : MonoBehaviour
{
    private bool isHovered = false;

    private void Update()
    {
        // Check if the controller is hovering over tg0 or tg1
        if (isHovered)
        {
            // Perform some action when the toggle is hovered
            Debug.Log("Toggle is being hovered with a controller.");
        }
    }

    public void OnToggle0Enter()
    {
        // Set isHovered to true when the controller enters the toggle's area
        isHovered = true;
    }

    public void OnToggle0Exit()
    {
        // Set isHovered to false when the controller exits the toggle's area
        isHovered = false;
    }
}