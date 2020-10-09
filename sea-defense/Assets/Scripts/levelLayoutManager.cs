using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class levelLayoutManager : MonoBehaviour
{
    public GameObject[] tiles;

    [SerializeField] private CameraBehaviour cameraBehaviour;
    public int xRes = Screen.width;
    public int yRes = Screen.height;
    public float tileSize 
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    public Dictionary<GridPoint, GridTile> Tiles { get; set; }    
    
    public GridPoint StartSpawn, EndSpawn;

    public GameObject startPrefab, endPrefab;

    public struct node
    {
        public int x;
        public int y;
    }
    public List<node> nodes = new List<node>();

    void Start()
    {
        LevelSetup();
    }

    void Update()
    {
    }

    private void LevelSetup()
    {
        //We create a tiles dictionary to memorize tile map
        Tiles = new Dictionary<GridPoint, GridTile>();
        string[] mapArray = ReadLevel();
        int xL = mapArray[0].ToCharArray().Length;
        int yL = mapArray.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 start = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
        //creates a grid based on grassTile
        for(int y = 0; y < yL; y++)
        {
            char[] newTiles = mapArray[y].ToCharArray();
            for (int x = 0; x < xL; x++)
            {
                PlaceTile(newTiles[x].ToString(),x,y,start);
            }
        }
        maxTile = Tiles[new GridPoint(xL-1,yL-1)].transform.position;
        cameraBehaviour.cameraLimits(new Vector3(maxTile.x + tileSize, maxTile.y - tileSize));
        CreateSpawn();
    }

    private void PlaceTile(string type, int x, int y, Vector3 start)
    {
            GridTile newTile = Instantiate(tiles[Translator(type)]).GetComponent<GridTile>();
            newTile.Setup(new GridPoint(x,y), new Vector3(start.x + (tileSize * x), start.y - (tileSize * y), 0));
            newTile.transform.parent = GameObject.Find("Grid").transform;
    }

    //Translates a char into a corresponding array integer
    private int Translator(string value)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>
        {
            {"a",0},{"b",1},{"c",2},{"d",3},{"e",4},{"f",5},{"g",6},{"h",7},
            {"i",8},{"j",9},{"k",10},{"l",11},{"m",12},{"n",13},{"o",14}
        };
        return dict[value];
    }

    private string[] ReadLevel()
    {
        //Load level text at a specified value
        int y = SceneManager.GetActiveScene().buildIndex;
        TextAsset tmp = Resources.Load("level" + y) as TextAsset;
        TextAsset tmpNodes = Resources.Load("nodes" + y) as TextAsset;

        string nodesTM = tmpNodes.text.Replace(Environment.NewLine, string.Empty);

        string[] nodes2 = nodesTM.Split('*');

        for(int i = 0; i<nodes2.Length; i++)
        {
            //Debug.Log(nodes2[i]);
            string[] tmpn2 = nodes2[i].Split(',');
            node tmpN;
            tmpN.x = int.Parse(tmpn2[0]);
            tmpN.y = int.Parse(tmpn2[1]);
            nodes.Add(tmpN);
        }
        //works
        Debug.Log(nodes[0].x);
        string text = tmp.text.Replace(Environment.NewLine, string.Empty);

        return text.Split('*');
    }
    
    private void CreateSpawn()
    {
        //Select startpoint
        StartSpawn = new GridPoint(nodes[0].x,nodes[0].y);
        GameObject startS = (GameObject)Instantiate(startPrefab, Tiles[StartSpawn].GetComponent<GridTile>().worldPos, Quaternion.identity);
        startS.name = "Start Spawn";
        //Select endpoint
        EndSpawn = new GridPoint(nodes[nodes.Count -1].x,nodes[nodes.Count -1].y);
        GameObject endS = (GameObject)Instantiate(endPrefab, Tiles[EndSpawn].GetComponent<GridTile>().worldPos, Quaternion.identity);
        endS.name = "End Spawn";
    }
}
