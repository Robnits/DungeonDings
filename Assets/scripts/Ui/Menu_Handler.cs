using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Handler : MonoBehaviour
{


    //scene manager which scene you currently in
    public int sceneBuildIndex;

    //changes the money at the begging of the scene
    

    //takes you to the upgrade ui scene

    public void Play()
    {
        // Changes Scene 
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
    
}
