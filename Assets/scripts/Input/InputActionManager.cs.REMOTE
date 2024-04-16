using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActionAsset;

    private void Awake()
    {
        inputActionAsset.Enable();
    }
    private void OnDestroy()
    {
        inputActionAsset.Disable();
    }
}
