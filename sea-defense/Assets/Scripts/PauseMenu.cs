using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isSettings;

    private void Update()
    {
        if(!GameObject.FindObjectOfType<GameManager>().isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            isPaused = !isPaused;
            if (isPaused && !isSettings)
            {
                activateMenu();
            }
            else
            {
                if(!isSettings)
                {
                    deactivateMenu();
                }
                if(isSettings)
                {
                    closeSettings();
                    deactivateMenu();
                }
            }
            }
        }
    }
    public void activateMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);    
    }
    public void deactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void OpenSettings()
    {
        isSettings = true;
        pauseMenu.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }
    public void closeSettings()
    {
        isSettings = false;
        optionsMenu.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
