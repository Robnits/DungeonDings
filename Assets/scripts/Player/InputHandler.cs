using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

    private GameObject player;
    public OpenEinstellungen einstellungen;
    public PhaseTrackScript phaseTrackScript;

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

        if (player != null && !isSettingOpen)
        {
            if (phaseTrackScript != null && phaseTrackScript.playerIsAllowedToMove)
            {
                player.GetComponent<Player_behjaviour>().Movement(horizontalInput, verticalInput);

                if (Input.GetMouseButtonDown(0))
                    player.GetComponent<Player_behjaviour>().Shooting();

                if (Input.GetKeyDown(KeyCode.Space))
                    player.GetComponent<Player_behjaviour>().Dash();

                if (Input.GetKeyDown(KeyCode.Q))
                    player.GetComponent<Player_behjaviour>().ThrowGranade();
                if (Input.GetKeyDown(KeyCode.E))
                    isInteracting = true;
                else
                    isInteracting = false;
            }
            else
            {
                player.GetComponent<Player_behjaviour>().LookAtPlayer(); ;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isSettingOpen)
        {
            einstellungen.OpenMenu();
            isSettingOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isSettingOpen)
        {
            einstellungen.OpenMenu();
            isSettingOpen = false;
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
