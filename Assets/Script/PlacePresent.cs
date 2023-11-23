using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePresent : BombController
{

    private List<List<int>> bitCoordinates;
    public float timer = 0;
    public float timePerDrop = 7f;
    public MapCoordinates mapCoordinates;

    //void Awake()
    //{
    //    explosionRadius = 1;
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > timePerDrop)
    //    {
    //        bitCoordinates = mapCoordinates.bitCoordinates;
    //        if (bombRemaining == bombAmount)
    //        {
    //            while (bombRemaining > 0)
    //            {
    //                bombRemaining--;
    //                StartCoroutine(PlaceBomb());
    //            }
    //        }
    //
    //        timer = 0;
    //    }
    //}
    //
    //private IEnumerator PlaceBomb()
    //{
    //    Vector2 pos = new Vector2(19, 16);
    //    // Debug.Log('x'+bitCoordinates[0].Count.ToString());
    //    Debug.Log(bitCoordinates.Count);
    //    while (bitCoordinates[(int)pos.x][(int)pos.y] == 1)
    //    {
    //        pos.y = Random.Range(0, bitCoordinates[0].Count);
    //        pos.x = Random.Range(0, bitCoordinates.Count);
    //    }
    //    
    //    Debug.Log(pos);
    //    pos.x -= 9;
    //    pos.y -= 5;
    //    Debug.Log(pos);
    //    
    //
    //    GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
    //
    //    yield return new WaitForSeconds(bombFuseTime);
    //    Destroy(bomb);
    //    bombRemaining= Mathf.Min(bombRemaining+1, bombAmount);
    //
    //    SetupExplode(bomb.transform.position);
    //    
    //    Destroy(bomb.gameObject);
    //    
    //}
}
