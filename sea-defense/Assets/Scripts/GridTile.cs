using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    GameManager gm;
    public GridPoint gridPos {get; set; }

    //Colours for hoverovers
    public bool IsEmpty { get; set; }
    private Color32 fullColor = new Color32(255,118,118,255);
    private Color32 emptyColor = new Color32(96,255,90,255);
    private SpriteRenderer spriteRenderer;

    private Tower myTower;

    public Vector2 worldPos 
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x/2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
        }
    }
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void Setup(GridPoint gridPos, Vector3 worldPos){
        IsEmpty = true;
        this.gridPos = gridPos;
        transform.position = worldPos;
        levelLayoutManager lm = GameObject.FindObjectOfType<levelLayoutManager>();
        lm.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && gm.clicked!=null)
        {
            if(IsEmpty)
            {
                ColorTile(emptyColor);
            }
            if(!IsEmpty || gameObject.tag == "Path") ColorTile(fullColor);
            else if(Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject() && gm.clicked==null && Input.GetMouseButtonDown(0))
        {
            if(myTower != null)
            {
                gm.SelectTower(myTower);
            }
            else
            {
                gm.DeselectTower();
            }
        }
    }
    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        GameObject tower = (GameObject)Instantiate(gm.clicked.TowerPrefab,transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = gridPos.Y;
        tower.transform.SetParent(transform);
        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
        tower.name = gm.clicked.TowerPrefab.name;
        ColorTile(Color.white);
        IsEmpty = false;
        myTower.Price = gm.clicked.Price;
        gm.BuildTower();
    }

    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
