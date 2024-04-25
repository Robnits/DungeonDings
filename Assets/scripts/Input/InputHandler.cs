using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private GameObject player;
    private bool isInteracting;
    private Vector2 movement;

    [SerializeField]
    private OpenEinstellungen einstellungenHandler;
    public InputActionReference shootInput;
    public InputActionReference dashInput;
    public InputActionReference grenadeInput;
    public InputActionReference settingInput;
    public InputActionReference movementInput;
    public InputActionReference interactInput;
    public InputActionReference mousePosition;
    public InputActionReference coursorPosition;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null || einstellungenHandler.settingsIsOpen == true)
        {
            shootInput.action.performed += OnShoot;
            dashInput.action.performed += OnDash;
            grenadeInput.action.performed += OnGrenade;
            interactInput.action.performed += OnInteract;
            settingInput.action.performed += OnSettings;
            coursorPosition.action.performed += OnCoursorPositionChanged;
            mousePosition.action.performed += OnMousePositionChanged;
        }
    }

    private void OnDestroy()
    {
        if(player != null || einstellungenHandler.settingsIsOpen == true)
        {
            shootInput.action.performed -= OnShoot;
            dashInput.action.performed -= OnDash;
            grenadeInput.action.performed -= OnGrenade;
            interactInput.action.performed -= OnInteract;
            settingInput.action.performed -= OnSettings;
            coursorPosition.action.performed -= OnCoursorPositionChanged;
            mousePosition.action.performed -= OnMousePositionChanged;
        }
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null)
            if(!einstellungenHandler.settingsIsOpen)
                player.GetComponent<PlayerBehaviour>().Shooting();
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            if(!einstellungenHandler.settingsIsOpen)
                player.GetComponent<PlayerBehaviour>().Dash();
    }

    private void OnGrenade(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            if(!einstellungenHandler.settingsIsOpen)
                player.GetComponent<PlayerBehaviour>().ThrowGranade();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            isInteracting = true;
    }

    private void OnCoursorPositionChanged(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            player.GetComponent<PlayerBehaviour>().LookAtPlayerCoursor(ctx.ReadValue<Vector2>());
    }
    private void OnMousePositionChanged(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            player.GetComponent<PlayerBehaviour>().LookAtPlayerMouse(Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>()));
    }

    private void OnSettings(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if(player != null || einstellungenHandler.settingsIsOpen == true)
            if (einstellungenHandler.settingsIsOpen)
                einstellungenHandler.OpenSettings(0);
            else
                einstellungenHandler.OpenSettings(1);
    }

    private void Update()
    {
        if(player != null)
        {
            if(movementInput != null)
                movement = movementInput.action.ReadValue<Vector2>();
            player.GetComponent<PlayerBehaviour>().MousePosition(Camera.main.ScreenToWorldPoint(mousePosition.action.ReadValue<Vector2>()));
            if(!einstellungenHandler.settingsIsOpen)
                HandlePlayerInput();
        }
    }

    private void HandlePlayerInput()
    {
        if(player != null)
            player.GetComponent<PlayerBehaviour>().Movement(movement.x, movement.y);
        if(interactInput != null)
            isInteracting = interactInput.action.triggered;
    }

    public bool IsPlayerInteracting() 
    {
        return isInteracting;
    }
}