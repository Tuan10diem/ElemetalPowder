using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCoordinates : MonoBehaviour
{

    public Tilemap destructibles;
    public Tilemap undestructibles;

    public Vector2Int different = new Vector2Int(2, 0);
    
    public List<List<int>> bitCoordinates = new List<List<int>>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 100; i++)
        {
            List<int> tmp= new List<int>();
            for (int j = 0; j < 100; j++)
            {
                tmp.Add(1);
            }
            bitCoordinates.Add(tmp);
        }
        
        SetStatusTile(destructibles);
        SetStatusTile(undestructibles);
        
    }

    private void SetStatusTile(Tilemap tilemap)
    {
        if (tilemap == null) return;
        
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        
        // Debug.Log(bounds);
        
        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    int tmpX = Set00(x, y).x;
                    int tmpY = Set00(x, y).y;
                    bitCoordinates[tmpX][tmpY] = 0;
                    
                    // Debug.Log(tile);
                    // Debug.Log(x+", "+y);
                }
            }
        }
        
        
    }

    private Vector2Int Set00(int x, int y)
    {
        return new Vector2Int(x - different.x, y - different.y);
    }

}
