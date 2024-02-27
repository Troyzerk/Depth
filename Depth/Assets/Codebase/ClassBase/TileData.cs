using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class TileData
{
    // getting data
    public List<Tile> refTiles;

    // passing data
    public TileBase tile;
    public bool isBlocking;
    public Vector3 position;
    public bool spawnOccupied;
}


