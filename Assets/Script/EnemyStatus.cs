using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Subjects
{

    public int HP;
    public float speedInit;
    public int damage;
    public float attackSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleHurt(int dam)
    {
        HP -= dam;
    }
    
}
