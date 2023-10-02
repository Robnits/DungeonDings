using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public int sceneBuildIndex;
    public void Start()
    {
        sceneBuildIndex = 0;
    }
    public void nextlevel()
    {
        sceneBuildIndex++;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
    public void firstlevel()
    {
        sceneBuildIndex = 2;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
    public void mainmenu()
    {
        sceneBuildIndex = 1;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

    }
    public void shop()
    {
        sceneBuildIndex = 0;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextlevel();
        }
    }
}