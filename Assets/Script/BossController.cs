using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Subjects
{
    public int HP;
    public float restTime;
    public int currentAction = -1;
    public List<int> dameSkill;
    public List<float> skillTime;
    public List<bool> isCasting;
    public List<GameObject> skillList;
    public bool isAngry = false;
    public float timer = 0;

    private int currentHP;

    void Start()
    {
        NextAction();
    }

    void Update()
    {
        timer += Time.deltaTime;
        Skill();
        if (currentHP < HP / 2.0 && !isAngry)
        {
            AngryTime();
            isAngry = true;
        }
    }

    protected void NextAction()
    {
        currentAction += 1;
        if (currentAction >= skillTime.Count)
        {
            currentAction = 0;
        }
        StartCoroutine(Rest(restTime));
    }

    public void Skill()
    {
        if (isCasting[currentAction] && timer < skillTime[currentAction])
        {
            skillList[currentAction].SetActive(true);
            //NotifyObservers(BossAction.Skill1);
        }
        else
        {
            isCasting[currentAction] = false;
            skillList[currentAction].SetActive(false);
            timer = 0;
            NextAction();
        }
    }

    public void HandleHurt(int damage)
    {
        currentHP -= damage;
    }

    private IEnumerator Rest(float restTime)
    {
        yield return new WaitForSeconds(restTime);
        timer = 0;
        isCasting[currentAction] = true;
    }

    private void AngryTime()
    {
        restTime = (restTime / 3)*2;
        for(int i=0;i<skillTime.Count;i++)
        {
            skillTime[i] *= 3 / 2;
        }
    }
}
