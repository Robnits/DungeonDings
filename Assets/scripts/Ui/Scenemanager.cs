using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    private Animator LevelLoaderAnim;

    private void Awake()
    {
        LevelLoaderAnim =  GameObject.Find("Transition").GetComponentInChildren<Animator>();
    }

    public void StartSwitch(int scene)
    {
        if (scene == 1)
            Time.timeScale = 1f;
        StartCoroutine(Scenswitch(scene));
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private IEnumerator Scenswitch(int scene)
    {
        LevelLoaderAnim.SetTrigger("Start");
        yield return new WaitForSeconds(0.6f);
        switch (scene)
        {
            case 0:
                SceneManager.LoadScene("Shop", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case 2:
                GlobalVariables.isInBossFight = false;
                SceneManager.LoadScene("procedural_dungeon", LoadSceneMode.Single);
                break;
            case 3:
                SceneManager.LoadScene("MarcelBossfight", LoadSceneMode.Single);
                break;
            case 4:
                SceneManager.LoadScene("Death", LoadSceneMode.Single);
                break;
            case 5:
                SceneManager.LoadScene("Victory", LoadSceneMode.Single);
                break;
        }
    }
}