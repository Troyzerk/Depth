using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class WorldGeneratorManager : MonoBehaviour
{

    
    [SerializeField]
    private bool debugTileData = false;

    public int cellXAmount;
    public int cellYAmount;

    public float scale = 20f;
    public float blockerClamp = 0.6f;

    float offsetX = 100f;
    float offsetY = 100f;

    public static GameObject tileGrid;
    GameObject tileMap;
    GameObject tileMapBlocker;
    Tilemap tilemapBlockerComp;
    Tilemap tileMapComp;

    public static GameObject tileGridL1;


    public TileBase nonblockerTile;
    public TileBase blockerTile;

    [SerializeField]
    public List<TileData> tileDatas;
    public List<TileData> blockingTileDatas;
    public List<TileData> nonblockingTileDatas;


    private Dictionary<TileBase, TileData> dataFromTiles;


    public void CreateWorld()
    {
        print("Creating world ");

        dataFromTiles = new Dictionary<TileBase, TileData>();
        tileDatas = new List<TileData>();
        blockingTileDatas = new List<TileData>();
        nonblockingTileDatas = new List<TileData>();
        

        RandomiseValues();
        InitializeWorldGrids();
        AddTiles();

        SpawnGameplayObjects();
    }

    public void SpawnGameplayObjects()
    {
        //Spawn things
        NPCPartySpawner.SpawnNPCGroups(10);
        NPCPartySpawner.SpawnLandmark(20);
        SpawnSomething(5, Resources.Load("Gameplay/Town") as GameObject, GameObject.Find("PersistentManager/Towns").transform, nonblockingTileDatas);
        SpawnSomething(1, Resources.Load("Gameplay/HordeDen") as GameObject, GameObject.Find("PersistentManager/Towns").transform, nonblockingTileDatas);
    }
    public void RandomiseValues()
    {
        offsetX = Random.Range(0, 999999);
        offsetY = Random.Range(0, 999999);
        print($"X = {offsetX}");
        print($"Y = {offsetY}");

    }

    public void InitializeWorldGrids()
    {
        tileGrid = InitGrid(tileGrid);

        // Inits all tilemaps 
        tileMap = InitTileMap(tileGrid, tileMap, tileMapComp, false);
        tileMapBlocker = InitTileMap(tileGrid, tileMapBlocker, tilemapBlockerComp, true);
        tileMapBlocker.name = "TileMapBlocker";

        // Inits all tilemap components
        tileMapComp = tileMap.GetComponent<Tilemap>();
        tilemapBlockerComp = tileMapBlocker.GetComponent<Tilemap>();



        tileGrid.transform.position = new Vector3((cellXAmount / 4)*-1, (cellYAmount / 4) * -1);

        DontDestroyOnLoad(tileGrid);
    }
    public GameObject InitTileMap(GameObject parent,GameObject objTileMap, Tilemap tileMapComp, bool hasCollision)
    {
        objTileMap = new();
        objTileMap.name = "TileMap";
        objTileMap.AddComponent<Tilemap>();
        objTileMap.AddComponent<TilemapRenderer>().sortingOrder = -10;
        if (hasCollision)
        {
            //objTileMap.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            objTileMap.AddComponent<TilemapCollider2D>();
        }
        objTileMap.transform.SetParent(parent.transform);
        return objTileMap;
    }
    public GameObject InitGrid(GameObject grid)
    {
        grid = new();
        grid.name = "grid";
        grid.AddComponent<Grid>();
        grid.GetComponent<Grid>().cellSize = new Vector3(0.5f, 0.5f, 0);
        return grid;
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
                print(newTile);
                Vector3 worldPosition = tileMapComp.GetCellCenterWorld(new Vector3Int(x, y, 0));
                
                newTile.position = worldPosition;
                tileDatas.Add(newTile);

                if (CalculateTileType(x, y) > blockerClamp)
                {
                    tilemapBlockerComp.SetTile(new Vector3Int(x, y, 0), blockerTile);
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
                //print(newObject.name + " : Spawned at : " + randomTile.position + " ||| The tile is marked as " + randomTile.isBlocking);
                i++;
            }

        }
    }

}
