using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private bool debugTileData = false;

    public int cellXAmount;
    public int cellYAmount;




    public float scale = 20f;
    public float blockerClamp = 0.6f;

    float offsetX = 100f;
    float offsetY = 100f;
    GameObject tileGrid;
    GameObject tileMap;
    Tilemap tileMapComp;

    public TileBase nonblockerTile;
    public TileBase blockerTile;

    [SerializeField]
    public List<TileData> tileDatas;
    public List<TileData> blockingTileDatas;
    public List<TileData> nonblockingTileDatas;





    private Dictionary<TileBase, TileData> dataFromTiles;

    // Start is called before the first frame update
    public void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        tileDatas = new List<TileData>();
        blockingTileDatas = new List<TileData>();
        nonblockingTileDatas = new List<TileData>();

        RandomiseValues();
        InitializeWorld();
        AddTiles();

        //Spawn things
        NPCPartySpawner.SpawnNPCGroups(10);
        //SpawnTowns(5);
        NPCPartySpawner.SpawnLandmark(20);
        SpawnSomething(5, Resources.Load("Town") as GameObject, GameObject.Find("PersistantManager/Towns").transform,nonblockingTileDatas);


    }
    public void RandomiseValues()
    {
        offsetX = Random.Range(0, 99999999f);
        offsetY = Random.Range(0, 99999999f);
    }

    public void InitializeWorld()
    {
        tileGrid = new();
        tileGrid.name = "TileGrid";
        tileGrid.AddComponent<Grid>();
        tileGrid.GetComponent<Grid>().cellSize = new Vector3(0.5f, 0.5f, 0);

        tileMap = new();
        tileMap.name = "TileMap";
        tileMapComp = tileMap.AddComponent<Tilemap>();
        tileMap.AddComponent<TilemapRenderer>().sortingOrder = -100;
        tileMap.transform.SetParent(tileGrid.transform);

        tileGrid.transform.position = new Vector3((cellXAmount / 4)*-1, (cellYAmount / 4) * -1);
    }

    public void AddTiles()
    {
        // adds tiles to map and stores data
        for (int x = 0; x < cellXAmount; x++)
        {
            for (int y = 0; y < cellYAmount; y++)
            {
                TileData newTile = new();
                newTile.tile = tileMapComp.GetTile(new Vector3Int(x, y, 0));
                Vector3 worldPosition = tileMapComp.GetCellCenterWorld(new Vector3Int(x, y, 0));
                newTile.position = worldPosition;
                tileDatas.Add(newTile);

                if (CalculateTileType(x, y) > blockerClamp)
                {
                    tileMapComp.SetTile(new Vector3Int(x, y, 0), blockerTile);
                    blockingTileDatas.Add(newTile);
                }
                else
                {
                    tileMapComp.SetTile(new Vector3Int(x, y, 0), nonblockerTile);
                    nonblockingTileDatas.Add(newTile);
                }
            }
        }



        // adds tiles to dictionary to query 
        foreach (TileData tileData in tileDatas)
        {
            if (tileData.refTiles != null)
            {
                foreach (var tile in tileData.refTiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }
            
        }

        
    }

    public float CalculateTileType(int x, int y)
    {
        float xCoord = (float)x / cellXAmount * scale;
        float yCoord = (float)y / cellYAmount * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);


    }

    private void Update()
    {
        
        //Debugger for detecting tiledata
        
        if (debugTileData && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tileMapComp.WorldToCell(mousePos);

            TileBase clickedTile = tileMapComp.GetTile(gridPos);

            bool isBlocking = dataFromTiles[clickedTile].isBlocking;
            
            print("This tile is blocking : " + isBlocking);
        }
    }

    public void SpawnTowns(int count)
    {
        for (int i = 0; i < count;)
        {
            TileData randomTile = blockingTileDatas[Random.Range(0, blockingTileDatas.Count)];

            if(randomTile.spawnOccupied == false)
            {
                GameObject newtown = Instantiate(Resources.Load("Town") as GameObject, GameObject.Find("PersistantManager/Towns").transform);
                
                newtown.gameObject.transform.position = randomTile.position;
                randomTile.spawnOccupied = true;
                print(newtown.name + " : Spawned at : " + randomTile.position);
                i++;
            }
            
        }
    }



    public void SpawnSomething(int count, GameObject objectToSpawn, Transform parentTransform, List<TileData> tileArrayToSpawnOn)
    {
        for (int i = 0; i < count;)
        {
            TileData randomTile = tileArrayToSpawnOn[Random.Range(0, tileArrayToSpawnOn.Count)];

            if (randomTile.spawnOccupied == false)
            {
                GameObject newObject = Instantiate(objectToSpawn,parentTransform);

                newObject.gameObject.transform.position = randomTile.position;
                randomTile.spawnOccupied = true;
                print(newObject.name + " : Spawned at : " + randomTile.position + " ||| The tile is marked as " + randomTile.isBlocking);
                i++;
            }

        }
    }

}
