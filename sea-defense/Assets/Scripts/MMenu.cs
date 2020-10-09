using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMenu : MonoBehaviour
{
    public GameObject menu;
   public GameObject settings;
   public GameObject newgame;
   public GameObject credits;

   public int selectedLevel = 1;

    public void setNumber(int n)
    {
        selectedLevel = n;
        Debug.Log("Now loading " + selectedLevel.ToString());
    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(selectedLevel);
    }
    public void OpenSettings ()
    {
        settings.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
    public void closeSettings()
    {
        settings.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void OpenNewGameMenu()
    {
        Debug.Log("Opening");
        Debug.Log("dasd");
        newgame.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
    public void closeNewGameMenu()
    {
        newgame.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }
    public void OpenCredits()
    {
        credits.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
    public void closeCredits()
    {
        credits.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

}
