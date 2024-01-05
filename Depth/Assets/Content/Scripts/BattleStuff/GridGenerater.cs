using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerater : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tileCharaPrefab;
    
    [SerializeField] private Grid grid; 
    [SerializeField] private Transform _cam; 
    
    private GameObject spawnTile;

    private Vector3 worldPosition; 
    

    void Start()
    {
        GererateGrid();
        
    }

    void GererateGrid()
    {
        for (int x = 0; x<width;x++)
        {
            for (int y = 0; y<height;y++)
            {
                worldPosition = grid.GetCellCenterWorld(new Vector3Int(x,y));
                spawnTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity,gameObject.transform); //added gameObject.transform so that the gridcells take this game object as a partent
                spawnTile.name = $"Tile{x}{y}";
                if (x >= 7)
                {
                    spawnTile.GetComponent<BoxCollider2D>().enabled = false ;
                }
            }
        }
        _cam.transform.position = new Vector3 ((float) width/2, (float) height/2,-10);

        for (int y = 0; y<height;y++)
        {
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(-2,y));
            spawnTile = Instantiate(tileCharaPrefab, worldPosition, Quaternion.identity,gameObject.transform);//added gameObject.transform so that the gridcells take this game object as a partent

            spawnTile.name = $"Tile{-1.5}{y}";
        }
    }
}
