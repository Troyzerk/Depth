using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class GridGenerater : MonoBehaviour
{
    [SerializeField] private float width, height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tileCharaPrefab;
    
    [SerializeField] private Grid grid; 
    [SerializeField] private Transform _cam; 
    
    private GameObject spawnTile;

    private Vector3 worldPosition; 
    

    void Start()
    {
        //GererateGrid();
        
    }

    public void GererateGrid()
    {
        for (int x = 0; x<width;x++)
        {
            for (int y = 0; y<height;y++)
            {
                worldPosition = grid.GetCellCenterWorld(new Vector3Int(x,y));
                spawnTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity,gameObject.transform); //added gameObject.transform so that the gridcells take this game object as a partent
                spawnTile.name = $"Tile{x}{y}";
                if (x >= 8)
                {
                    spawnTile.GetComponent<BoxCollider2D>().enabled = false ;
                }
            }
        }

        Vector3 CameraPos = new Vector3((float)width / 2, (float)height / 2, -10);
        Vector3 offSet = new Vector3(0, (float)0.5, 0);
        _cam.transform.position = CameraPos - offSet ;

        for (int x = 1; x< (width-1); x++)
        {
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(x, -1));
            spawnTile = Instantiate(tileCharaPrefab, worldPosition, Quaternion.identity,gameObject.transform);//added gameObject.transform so that the gridcells take this game object as a partent

            spawnTile.name = $"Tile{x}{-.5}";
        }
    }
}
