using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillAnimated
{
    public AnimatedSpriteRenderer pre, casting, post;
}

public class BossController : Subjects
{
    public int HP;
    public float restTime;
    public int currentAction;
    public List<int> dameSkill;
    public List<float> skillTime;
    public List<bool> isCasting;
    public List<GameObject> skillList;
    public bool isAngry = false;
    public float timer = 0;

    private int currentHP;
    private bool first = true;

    [SerializeField]
    public List<SkillAnimated>  listSpritesRendererSkill;
    public AnimatedSpriteRenderer spriteRendererIdle;

    // public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    void Start()
    {


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
            listSpritesRendererSkill[currentAction].casting.enabled  = true;
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
        listSpritesRendererSkill[(currentAction==0)? (skillList.Count-1):(currentAction-1)].post.enabled = true;
        listSpritesRendererSkill[(currentAction == 0) ? (skillList.Count - 1) : (currentAction - 1)].casting.enabled = false;
        listSpritesRendererSkill[(currentAction == 0) ? (skillList.Count - 1) : (currentAction - 1)].pre.enabled = false;

        yield return new WaitForSeconds(0.25f);

        listSpritesRendererSkill[(currentAction == 0) ? (skillList.Count - 1) : (currentAction - 1)].post.enabled = false;
        spriteRendererIdle.enabled = true;

        yield return new WaitForSeconds(restTime - 0.5f);

        spriteRendererIdle.enabled = false;
        timer = 0;
        listSpritesRendererSkill[currentAction].pre.enabled = true;

        yield return new WaitForSeconds(0.25f);

        listSpritesRendererSkill[currentAction].pre.enabled = false;
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
