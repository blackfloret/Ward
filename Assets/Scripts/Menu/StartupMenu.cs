using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject panel;
    public GameObject controlPanel;

    void Start()
    {
        panel.SetActive(true);
        controlPanel.SetActive(false);
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest v3");
    }

    public void Controls()
    {
        controlPanel.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }

    public void Back()
    {
        controlPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
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
