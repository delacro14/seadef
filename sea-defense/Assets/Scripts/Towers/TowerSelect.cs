using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;
    [SerializeField]
    private TextMeshProUGUI priceTxt;
    public string type;

    public GameObject TowerPrefab { get => towerPrefab; }
    public Sprite Sprite { get => sprite; }
    public int Price { get => price; set => price = value; }
    
    private void Update()
    {
        if(GameObject.FindObjectOfType<GameManager>().checkUnlock(this) && type !=  "fire")
        {
            Destroy(gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        if(price <= GameObject.FindObjectOfType<GameManager>().Gold && GameObject.FindObjectOfType<GameManager>().checkUnlock(this))
        {
            GetComponent<Image>().color = Color.white;
            priceTxt.color = Color.white;
        }
        else 
        {
            GetComponent<Image>().color = Color.grey;
            priceTxt.color = Color.grey;
        }
    }
    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;
        Tower tower = towerPrefab.GetComponentInChildren<Tower>();
        switch(type)
        {
            case "fire":
                tooltip = string.Format("<color=#ffa500ff><size=18><b>Fire Tower</b></size></color>\nDamage: {0} \n Attack Speed: {1}\nRange: {2}\n-------------\nDebuff chance: {3}%\nDebuff duration: {4}sec\nDebuff damage: {5}\n <color=#cb3b3b><size=16><b><i>chance to burn the ship</i></b></size></color>", tower.Damage, tower.AttackCooldown, tower.FiringRange, tower.Proc, tower.DebuffDuration, tower.TickDamage);
                break;
            case "ice":
                tooltip = string.Format("<color=#00ffffff><size=18><b>Ice Tower</b></size></color>\nDamage: {0} \nAttack Speed: {1}\nRange: {2}\n-------------\nDebuff chance: {3}%\nDebuff duration: {4}sec\nSlow %: {5}\n <color=#2EA3D1><size=16><b><i>chance to slow the ship</i></b></size></color>", tower.Damage, tower.AttackCooldown, tower.FiringRange, tower.Proc,tower.DebuffDuration, tower.SlowFactor);
                break;
            case "poison":
                tooltip = string.Format("<color=#00ff00ff><size=18><b>Poison Tower</b></size></color>\nDamage: {0} \n Attack Speed: {1}\nRange: {2}\n-------------\nDebuff chance: {3}%\nDebuff duration: {4}sec\nSplash damage: {5}\n <color=#239822><size=16><b><i>chance to drop a splash</i></b></size></color>", tower.Damage, tower.AttackCooldown, tower.FiringRange, tower.Proc, tower.DebuffDuration, tower.TickDamage);    
                break;
            case "storm":
                tooltip = string.Format("<color=#add8e6ff><size=18><b>Storm Tower</b></size></color>\nDamage: {0} \n Attack Speed: {1}\nRange: {2}\n-------------\nDebuff chance: {3}%\nStun duration: {4}sec\n <color=#D2AB2F><size=16><b><i>chance to stun the ship</i></b></size></color>", tower.Damage, tower.AttackCooldown, tower.FiringRange, tower.Proc, tower.DebuffDuration);                
                break;
            default:
                break;
        }
        GameObject.FindObjectOfType<GameManager>().setTooltipText(tooltip);
        GameObject.FindObjectOfType<GameManager>().ShowStats();
    }
}
