using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Subjects
{
    public float restTime;
    public int currentAction = -1;
    public List<float> skillTime;
    public List<bool> isCasting;
    public List<GameObject> skillList;
    public float timer = 0;
    
    void Start()
    {
        NextAction();
    }
    
    void Update()
    {
        timer+=Time.deltaTime;
        Skill();
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
        if (isCasting[currentAction] && timer<skillTime[currentAction])
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
    
    private IEnumerator Rest(float restTime)
    {
        yield return new WaitForSeconds(restTime);
        timer = 0;
        isCasting[currentAction] = true;
    }
    
}
