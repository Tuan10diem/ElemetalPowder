using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D _rigidbody2D;

    public Vector2 direction;
    public MapCoordinates grid;
    public List<List<int>> bitCoordinates = new List<List<int>>();

    private int[] dX = { 1, 0, -1, 0 };
    private int[] dY = { 0, 1, 0, -1 };
    private int pathIndex = 0;
    private List<Vector2> path = new List<Vector2>();
    private Vector2 nextPos;

    private Vector2Int rootPoint = new Vector2Int(-9, -5);
    // Start is called before the first frame update

    public float speed = 5f;

    private void Awake()
    {
        this.bitCoordinates = grid.bitCoordinates;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        int x = (int)Mathf.Round(transform.position.x) - rootPoint.x;
        int y = (int)Mathf.Round(transform.position.y) - rootPoint.y;
        // Debug.Log(x+","+y);
        DFS(x, y);
        // foreach (var i in path)
        // {
        //     Debug.Log(i);
        // }

        nextPos = path[0];

        // UpdatePosition(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (Vector2.Distance(transform.position, nextPos+rootPoint) < 0.001f)
        {
            pathIndex++;
            if (pathIndex >= path.Count)
            {
                // pathIndex = 0;
            }
            Debug.Log("ngu");
            nextPos = path[pathIndex];
        }
        else
        {
            transform.position =
                Vector2.MoveTowards(transform.position, nextPos+rootPoint, speed * Time.deltaTime);
        }
    }

    private void DFS(int x, int y)
    {
        // Debug.Log(x+", "+y);
        bitCoordinates[x][y] = 0;
        path.Add(new Vector2(x, y));
        for (int i = 0; i < 4; i++)
        {
            if (x + dX[i] < 0 || y + dY[i] < 0) continue;
            if (bitCoordinates[x + dX[i]][y + dY[i]] == 1)
            {
                DFS(x + dX[i], y + dY[i]);
                path.Add(new Vector2(x, y));
            }
        }
    }
}