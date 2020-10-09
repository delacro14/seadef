using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchButton : MonoBehaviour
{
    [SerializeField]
    private int price;
    [SerializeField]
    private string type;
    private bool isUnlocked = false;
    [SerializeField]
    private TextMeshProUGUI textUpgrade;

    void Update()
    {
        if (price > GameObject.FindObjectOfType<GameManager>().XP && !isUnlocked)
        {
            gameObject.GetComponent<Button>().interactable = false;
            textUpgrade.text = "Missing XP";
        }
        else if(isUnlocked)
        {
            gameObject.GetComponent<Button>().interactable = false;
            textUpgrade.text = "Unlocked";

        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
            textUpgrade.text = "Unlock";
        }
    }
    public void UnlockTower()
    {
        if (GameObject.FindObjectOfType<GameManager>().XP >= price && !isUnlocked)
        { 
            GameObject.FindObjectOfType<GameManager>().XP -= price;
            isUnlocked = true;
            switch (type)
            {
                case "ice":
                    GameObject.FindObjectOfType<GameManager>().iceUnlocked = true;
                    Debug.Log("Unlocked ice turret");
                    break;
                case "poison":
                    GameObject.FindObjectOfType<GameManager>().poisonUnlocked = true;
                    break;
                case "storm":
                    GameObject.FindObjectOfType<GameManager>().stormUnlocked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
