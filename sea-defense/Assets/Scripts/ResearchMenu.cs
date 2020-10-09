using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchMenu : MonoBehaviour
{
    public GameObject unlockmenu;


    public void OpenUnlockMenu()
    {
        unlockmenu.gameObject.SetActive(true);
    }
    public void closeUnlockMenu()
    {
        unlockmenu.gameObject.SetActive(false);
    }
}
