using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{

    public List<Vector3Int> path;
    public Vector3Int nextPos;
    public int pathIndex = 0;
    public float speed = 1f;
    public BulletController bulletController;
    public float timer = 0;
    public float attackSpeed = 2f;

    // Start is called before the first frame update
    //void Start()
    //{
    //    path = new List<Vector3Int>{
    //    new Vector3Int(0,0,0),
    //    new Vector3Int(5,0,0),
    //    new Vector3Int(5,2,0),
    //    new Vector3Int(0,2,0)};
    //    
    //    nextPos = path[1];
    //}

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, nextPos) < 0.001f)
        //{
        //    pathIndex++;
        //    if (pathIndex >= 4)
        //    {
        //        pathIndex = 0;
        //    }
        //    Debug.Log("ngu");
        //    nextPos = path[pathIndex];
        //}
        //else
        //{
        //    transform.position =
        //        Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        //}

        //Shooting();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(call());
        }
    }

    IEnumerator call()
    {
        Debug.Log("call1");
        yield return new WaitForSeconds(2f); 
    }

    //void Shooting()
    //{
    //    timer += Time.deltaTime;
    //    if (timer < attackSpeed) return;
    //    timer = 0;
    //    Instantiate(bulletController, this.transform.position, this.transform.rotation);
    //}
}
