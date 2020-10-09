using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TowerSelect clicked { get; set; }
    public int Gold 
    {
        get
        {
            return gold;
        } 
        set
        {
            this.gold = value;
            this.currency.text = value.ToString();
        }
    }
    public int XP
    {
        get
        {
            return xp;
        }
        set
        {
            this.xp = value;
            this.xpTxt.text = value.ToString();
            this.XPTxt.text = "XP: " + value.ToString();
        }
    }
    private int gold;
    private int xp;
    private int wave = 0;
    private int lives;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            this.lives = value;
            if(lives <= 0)
            {
                Time.timeScale = 0;
                this.lives = 0;
                GameOver();
            }

            this.livesTxt.text = lives.ToString();
        }
    }
    public bool isGameOver = false;
    private int maxWaves = 5;
    //enemy health
    private int health = 20;
    private string[] wavesVal;
    public bool iceUnlocked = false;
    public bool stormUnlocked = false;
    public bool poisonUnlocked = false;

    [SerializeField]
    private TextMeshProUGUI waveTxt;
    [SerializeField]
    private TextMeshProUGUI currency;
    [SerializeField]
    private TextMeshProUGUI xpTxt;

    [SerializeField]
    private TextMeshProUGUI livesTxt;
    [SerializeField]
    private TextMeshProUGUI XPTxt;
    [SerializeField]
    private TextMeshProUGUI upgradeTxt;

    [SerializeField]
    private TextMeshProUGUI sellTxt;

    [SerializeField]
    private TextMeshProUGUI statsTxt;


    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject victoryMenu;

    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private GameObject statsPanel;

    private List<Ship> shipObjects = new List<Ship>();

    public ObjectsPool Pool {get; set; }

    private Tower selectedTower;

    private int shipsLeft = 0;

    public bool isWaveActive
    {
        get { return shipObjects.Count > 0;}
    }
    void Awake()
    {
     Pool = GetComponent<ObjectsPool>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        Lives = 5;
        XP = 100;
        readWaves();
        maxWaves = wavesVal.Length;
        waveTxt.text = wave.ToString() + "/" + maxWaves.ToString();
        Gold = 400;
    }

    // Update is called once per frame
    void Update()
    {
        CancelBuild();
        CheckUpgrade();
    }
    public bool checkUnlock(TowerSelect towerSelect){
        switch(towerSelect.type)
        {
            case "ice":
                if(iceUnlocked) return true;
                else return false;
            case "storm":
                if(stormUnlocked) return true;
                else return false;
            case "poison":
                if(poisonUnlocked) return true;
                else return false;
            default:
                return true;
        }
    }
    public void TowerSelection(TowerSelect towerSelect)
    {
        DeselectTower();
        if(Gold >= towerSelect.Price && checkUnlock(towerSelect))
        {
            this.clicked = towerSelect;
            GameObject.FindObjectOfType<OnHover>().Activate(towerSelect.Sprite);
        }
    }
    public void BuildTower()
    {
        if(Gold >= clicked.Price)
        {
            Gold -= clicked.Price;
            GameObject.FindObjectOfType<OnHover>().Deativate();
        }
    }
    private void CancelBuild()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindObjectOfType<OnHover>().Deativate();
        }
    }
    public void CallWave()
    {
        wave++;
        waveTxt.text = wave.ToString() + "/" + maxWaves.ToString();
        StartCoroutine(SpawnWave());
        waveBtn.GetComponent<Button>().interactable = false;
    }
    private IEnumerator SpawnWave()
    {
        shipsLeft = wavesVal[wave-1].Length;
        for(int i = 0; i<wavesVal[wave-1].Length; i++)
        {
        string type = string.Empty;
        int shipIndex = int.Parse((wavesVal[wave-1][i]).ToString());
        switch(shipIndex)
        {
            case 0:
                type="EnemyShip";
                break;
            default:
                break;
        }
        Ship ship = Pool.GetObject(type).GetComponent<Ship>();
        ship.Spawn(health);
        shipObjects.Add(ship);
        yield return new WaitForSeconds(1.5f);
        }
    }
    private void readWaves(){
        int y = SceneManager.GetActiveScene().buildIndex;
        TextAsset tmp = Resources.Load("waves" + y) as TextAsset;

        string text = tmp.text.Replace(Environment.NewLine, string.Empty);
        wavesVal = text.Split('*');
    }
    public void removeShip(Ship ship)
    {
        shipsLeft--;
        Debug.Log("Ships left: " + shipsLeft);
        shipObjects.Remove(ship);
        if(!isWaveActive && !isGameOver && shipsLeft == 0)
        {
            XP+= 10 * wavesVal[wave-1].Length;
            waveBtn.GetComponent<Button>().interactable = true;
            if(wave == maxWaves && Lives > 0)
            {
                Victory();
            }
        }
    }
    private void GameOver()
    {
        if(!isGameOver)
        {
            Time.timeScale = 0;
            isGameOver = true;
            gameOverMenu.SetActive(true);
        }
    }
    private void Victory()
    {
        Time.timeScale = 0;
        victoryMenu.SetActive(true);
    }
    public void SelectTower(Tower tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();

        sellTxt.text = "+ " + (selectedTower.Price / 2).ToString();

        upgradePanel.SetActive(true);
    }
    public void DeselectTower()
    {
        if(selectedTower!= null)
        {
            selectedTower.Select();
        }
        upgradePanel.SetActive(false);
        selectedTower = null;
    }
    public void SellTower()
    {
        if (selectedTower != null)
        {
            Gold+= selectedTower.Price /2;
            selectedTower.GetComponentInParent<GridTile>().IsEmpty = true;
            Destroy(selectedTower.transform.parent.gameObject);
            DeselectTower();
        }
    }
    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }
    public void setTooltipText(string text)
    {
        statsTxt.text = text;
    }
    public void updateTooltip()
    {
        if(selectedTower != null)
        {
            sellTxt.text = "+ " + (selectedTower.Price/2).ToString();
          setTooltipText(selectedTower.getStats());  
          if(selectedTower.NextUpgrade != null)
          {
            upgradeTxt.text = selectedTower.NextUpgrade.Price.ToString();
          }
          else
          {
            upgradeTxt.text = "MAX"; 
          } 
        }
    }
    public void ShowSelectedTowerStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        updateTooltip();
    }
    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            if(selectedTower.Level <= selectedTower.Upgrades.Length && Gold >= selectedTower.NextUpgrade.Price)
            {
                selectedTower.Upgrade();
            }
        }
    }

    public void CheckUpgrade()
    {
        if(selectedTower != null)
        {
            if(selectedTower.NextUpgrade != null)
            {
                if(selectedTower.NextUpgrade.Price <= Gold)
                {
                    upgradePanel.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    upgradeTxt.color = Color.white;
                }
                else if (selectedTower.NextUpgrade.Price <= Gold || selectedTower.Upgrades.Length > 0)
                {
                    upgradePanel.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                    upgradeTxt.color = Color.grey;
                }
            }
            else
            {
                upgradePanel.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                upgradeTxt.color = Color.grey;
            }
        }
    }
}
