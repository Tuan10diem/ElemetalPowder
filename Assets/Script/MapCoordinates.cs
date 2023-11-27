using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCoordinates : MonoBehaviour
{
    public Tilemap groundMap;
    public Tilemap destructibleMap;
    public Tilemap indestructibleMap;
    public TileBase destructibleBox;
    public int numberOfPos;
    public int timeBetweenTileChange;

    public GameObject obstacle;

    private void Start()
    {
        InvokeRepeating("GenerateRandomPositions", 0f, timeBetweenTileChange); 
    }

    void GenerateRandomPositions()
    {
        if (groundMap != null && destructibleMap != null && indestructibleMap != null)
        {
            List<Vector3Int> destructibleBoxPositions = GetBoxPositions(destructibleMap);
            List<Vector3Int> indestructibleBoxPositions = GetBoxPositions(indestructibleMap);

            for (int i = 0; i < numberOfPos; i++)
            {
                BoundsInt groundBounds = groundMap.cellBounds;

                Vector3Int randomGroundPosition = GetRandomGroundPosition(groundBounds, destructibleBoxPositions, indestructibleBoxPositions);

                Vector3 worldPosition = groundMap.GetCellCenterWorld(randomGroundPosition);

                if (UnityEngine.Random.Range(0, 10) < 3)
                {
                    Instantiate(obstacle, worldPosition, Quaternion.identity);
                }
                else
                {
                    destructibleMap.SetTile(new Vector3Int((int)worldPosition.x, (int)worldPosition.y, (int)worldPosition.z), destructibleBox);
                }
            }
        }
    }

    List<Vector3Int> GetBoxPositions(Tilemap boxMap)
    {
        List<Vector3Int> boxPositions = new List<Vector3Int>();

        foreach (Vector3Int pos in boxMap.cellBounds.allPositionsWithin)
        {
            if (boxMap.HasTile(pos))
            {
                boxPositions.Add(pos);
            }
        }

        return boxPositions;
    }

    Vector3Int GetRandomGroundPosition(BoundsInt groundBounds, List<Vector3Int> destructibleBoxPositions, List<Vector3Int> indestructibleBoxPositions)
    {
        Vector3Int randomPosition;

        do
        {
            randomPosition = new Vector3Int(
                UnityEngine.Random.Range(groundBounds.x, groundBounds.x + groundBounds.size.x),
                UnityEngine.Random.Range(groundBounds.y, groundBounds.y + groundBounds.size.y),
                0
            );

        } while (destructibleBoxPositions.Contains(randomPosition) || indestructibleBoxPositions.Contains(randomPosition));

        return randomPosition;
    }

}
