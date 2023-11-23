using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Subjects
{

    public int HP;
    public float speedInit;
    public int damage;
    public float attackSpeed;
   
    public void HandleHurt(int dam)
    {
        HP -= dam;
    }
    
}
