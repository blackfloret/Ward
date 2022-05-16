using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private float pauseTimeScale;
    public GameObject panel;
    public GameObject controlPanel;

    private void Awake()
    {
        panel.gameObject.SetActive(false);
        controlPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf)
        {
            Unpause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !panel.activeSelf)
        {
            Pause();
        }
    }

    public void Pause()
    {
        panel.SetActive(true);
        pauseTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        panel.SetActive(false);
        Time.timeScale = pauseTimeScale;
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

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}