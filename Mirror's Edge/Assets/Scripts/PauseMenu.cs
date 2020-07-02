using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject DeathMenuUI;
    public bool isPaused;
    public Game_Manager gm;
    public DeathManager dm;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        Debug.Log("Menu");
        gm.Save();
        Time.timeScale = 1f;
        gm.LoadMenu();
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        gm.Save();
        Time.timeScale = 1f;
        gm.QuitGame();
    }

    public void Dead()
    {
        DeathMenuUI.SetActive(true);
    }
    public void Retry()
    {
        dm.RespawnCharacter();
        DeathMenuUI.SetActive(false);
    }
}
