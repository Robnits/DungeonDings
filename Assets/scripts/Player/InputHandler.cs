using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

    private GameObject player;
    [SerializeField]
    private OpenEinstellungen einstellungen;
    [SerializeField]
    private PhaseTrackScript phaseTrackScript;

    private bool isSettingOpen;
    private bool isInteracting;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "procedural dungeon"|| GlobalVariables.isInBossFight)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (player != null && !isSettingOpen && phaseTrackScript == null)
            HandlePlayerInput();
        
        else if(player != null && !isSettingOpen && phaseTrackScript != null)
            if (phaseTrackScript.playerIsAllowedToMove)
                HandlePlayerInput();

        HandleMenuInput();
    }
    private void HandlePlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Player_behjaviour playerBehaviour = player.GetComponent<Player_behjaviour>();

        playerBehaviour.Movement(horizontalInput, verticalInput);

        if (Input.GetMouseButtonDown(0))
            playerBehaviour.Shooting();

        if (Input.GetKeyDown(KeyCode.Space))
            playerBehaviour.Dash();

        if (Input.GetMouseButtonDown(1))
            playerBehaviour.ThrowGranade();

        isInteracting = Input.GetKeyDown(KeyCode.E);
    }

    private void HandleMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            einstellungen.OpenMenu();
            isSettingOpen = !isSettingOpen;
        }
    }
    public bool IsPLayerInteracting()
    {
        return isInteracting;
    }

    public void SetIsSettingOpen()
    {
        isSettingOpen = false;
    }
}
