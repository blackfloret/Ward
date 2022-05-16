using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject MainMenu;
    //public Player playerScript;

    void Update()
    {
        //if (!playerScript.alive)
        //{
            //MainMenu.SetActive(true);
        //}
    }

    public void Restart()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(playerScript.sceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        #endif
    }
}
