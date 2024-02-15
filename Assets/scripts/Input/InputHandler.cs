using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Player_behjaviour playerBehaviour;
    private bool isInteracting;
    private Vector2 movement;

    [SerializeField]
    private OpenEinstellungen einstellungenHandler;
    [SerializeField]
    private OpenEinstellungen einstellungen;

    public InputActionReference shootInput;
    public InputActionReference dashInput;
    public InputActionReference grenadeInput;
    public InputActionReference settingInput;
    public InputActionReference movementInput;
    public InputActionReference interactInput;
    public InputActionReference mousePosition;

    private void Start()
    {
        
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_behjaviour>();

        shootInput.action.performed += OnShoot;
        dashInput.action.performed += OnDash;
        grenadeInput.action.performed += OnGrenade;
        interactInput.action.performed += OnInteract;
        settingInput.action.performed += OnSettings;
    }

    private void OnDestroy()
    {
        shootInput.action.performed -= OnShoot;
        dashInput.action.performed -= OnDash;
        grenadeInput.action.performed -= OnGrenade;
        interactInput.action.performed -= OnInteract;
        settingInput.action.performed -= OnSettings;
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        playerBehaviour.Shooting();
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        playerBehaviour.Dash();
    }

    private void OnGrenade(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        playerBehaviour.ThrowGranade();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        isInteracting = true;
    }

    private void OnSettings(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (einstellungenHandler.settingsIsOpen)
            einstellungenHandler.OpenSettings(0);
        else
            einstellungen.OpenSettings(1);
    }

    private void Update()
    {
        movement = movementInput.action.ReadValue<Vector2>();
        playerBehaviour.LookAtPlayer(mousePosition.action.ReadValue<Vector2>());
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        playerBehaviour.Movement(movement);
        isInteracting = interactInput.action.triggered;
    }

    public bool IsPlayerInteracting()
    {
        return isInteracting;
    }
}