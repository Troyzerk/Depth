using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseWorldGenerator : MonoBehaviour
{
    [Header("Debug")]
    public bool debugTileData = false;
    public bool debugWorldGen = false;
    public bool manualOffsets = false;

    [Tooltip("Seed only applies when DebugWorldGen is turned on.")]
    public int seed = 1122221;

    [Header("Generation Settings")]
    public int cellXAmount = 25;
    public int cellYAmount = 25;
    public float scale = 20f;
    public float blockerClamp = 0.6f;
    public Vector3 cellSize = new Vector3(0.5f, 0.5f, 0);

    [Header("Settings")]
    public float offsetX = 100f;
    public float offsetY = 100f;

    [HideInInspector] public static GameObject tileGrid;
    [HideInInspector] public GameObject tileMap;
    [HideInInspector] public GameObject tileMapBlocker;
    [HideInInspector] public Tilemap tilemapBlockerComp;
    [HideInInspector] public Tilemap tileMapComp;

    [Header("Tile Assignment")]
    public TileBase nonblockerTile;
    public TileBase blockerTile;


    [HideInInspector] public List<TileData> tileDatas;
    [HideInInspector] public List<TileData> blockingTileDatas;
    [HideInInspector] public List<TileData> nonblockingTileDatas;


    [HideInInspector] public Dictionary<TileBase, TileData> dataFromTiles;

    //------------------------ EXECUTION ------------------------//
    public virtual void Start()
    {
        
    }
   
    public virtual void Update()
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

    public virtual void CreateWorld()
    {
        print("Creating world ");
        if (debugWorldGen)
        {
            Debug.LogWarning("Initial Loading without assigned seed. Random Seed State being assigned.");
            Random.InitState(seed);
        }

        dataFromTiles = new Dictionary<TileBase, TileData>();
        tileDatas = new List<TileData>();
        blockingTileDatas = new List<TileData>();
        nonblockingTileDatas = new List<TileData>();

        RandomiseOffsets();
        InitializeWorldGrids();
        AddTiles();

    }

    //------------------------ END OF EXECUTION ------------------------//



    public void RandomiseOffsets()
    {
        if (!manualOffsets)
        {
            offsetX = Random.Range(0, 9999999);
            offsetY = Random.Range(0, 9999999);
        }
        Debug.LogWarning($"Random Offsets : X = {offsetX} || Y = {offsetY}");
    }

    public void InitializeWorldGrids()
    {
        tileGrid = InitGrid(tileGrid);

        // Inits all tilemaps 
        tileMap = InitTileMap(tileGrid, tileMap, tileMapComp, false);
        tileMapBlocker = InitTileMap(tileGrid, tileMapBlocker, tilemapBlockerComp, true);
        tileMap.name = "TileMap";
        tileMapBlocker.name = "TileMapBlocker";

        // Inits all tilemap components
        tileMapComp = tileMap.GetComponent<Tilemap>();
        tilemapBlockerComp = tileMapBlocker.GetComponent<Tilemap>();

        tileGrid.transform.position = new Vector3((cellXAmount / 4) * -1, (cellYAmount / 4) * -1);
    }

    public GameObject InitTileMap(GameObject parent, GameObject objTileMap, Tilemap tileMapComp, bool hasCollision)
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
        grid.GetComponent<Grid>().cellSize = cellSize;
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
        float xCoord = ((float)x + offsetX) / cellXAmount * scale;
        float yCoord = ((float)y + offsetY) / cellYAmount * scale;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    public void SpawnSomething(int count, GameObject objectToSpawn, Transform parentTransform, List<TileData> tileArrayToSpawnOn)
    {
        for (int i = 0; i < count;)
        {
            TileData randomTile = tileArrayToSpawnOn[Random.Range(0, tileArrayToSpawnOn.Count)];

            if (randomTile.spawnOccupied == false)
            {
                GameObject newObject = Instantiate(objectToSpawn, parentTransform);

                newObject.gameObject.transform.position = randomTile.position;
                randomTile.spawnOccupied = true;
                i++;
            }

        }
    }
}
