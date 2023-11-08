using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : Subjects
{
    public float restTime;
    public List<KeyValuePair<BossAction,System.Action>> bossStatus;
    public int currentAction = -1;

    private void Awake()
    {
        bossStatus.Add(new KeyValuePair<BossAction,System.Action>(BossAction.Skill1, Skill1));
        bossStatus.Add(new KeyValuePair<BossAction,System.Action>(BossAction.Skill2, Skill2));
        bossStatus.Add(new KeyValuePair<BossAction,System.Action>(BossAction.Skill3, Skill3));
    }
    
    protected void NextAction()
    {
        StartCoroutine(Rest(restTime));
        currentAction += 1;
        if (currentAction >= bossStatus.Count)
        {
            currentAction = 0;
        }
        bossStatus[currentAction].Value();
    }

    public abstract void Skill1();

    public abstract void Skill2();

    public abstract void Skill3();

    private IEnumerator Rest(float restTime)
    {
        yield return new WaitForSeconds(restTime);
    }
    
}
