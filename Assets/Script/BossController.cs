using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public AnimatedSpriteRenderer pre, casting, post;
    public int damage;
    public float time;
    public GameObject skill;
}

public class BossController : Subjects
{
    public int HP;
    public float restTime;
    public int currentAction;
    public bool isAngry = false;

    private int currentHP;

    public List<Skill>  listSkill;
    public AnimatedSpriteRenderer spriteRendererIdle;

    // public AnimatedSpriteRenderer spriteRendererDeath;

    void Start()
    {
        for (int i = 0; i < listSkill.Count; i++)
        {
            listSkill[i].pre.enabled = false;
            listSkill[i].casting.enabled = false;
            listSkill[i].post.enabled = false;
            listSkill[i].skill.SetActive(false);
        }

        StartCoroutine(UseSkillsRoutine());
    }

    private IEnumerator UseSkillsRoutine()
    {
        while (true)
        {
            //rest
            spriteRendererIdle.enabled = true;
            yield return new WaitForSeconds(restTime);
            spriteRendererIdle.enabled = false;

            //pre
            spriteRendererIdle.enabled = false;
            listSkill[currentAction].pre.enabled = true;
            yield return new WaitForSeconds(listSkill[currentAction].pre.animationTime);
            listSkill[currentAction].pre.enabled = false;

            //casting
            listSkill[currentAction].casting.enabled = true;
            listSkill[currentAction].skill.SetActive(true);
            yield return new WaitForSeconds(listSkill[currentAction].time);
            listSkill[currentAction].casting.enabled = false;
            listSkill[currentAction].skill.SetActive(false);

            //post
            listSkill[currentAction].casting.enabled = false;
            listSkill[currentAction].post.enabled = true;
            yield return new WaitForSeconds(listSkill[currentAction].post.animationTime);
            listSkill[currentAction].post.enabled = false;

            NextAction();
        }
    }

    private void AngryTime()
    {
        restTime = (restTime / 3) * 2;
        for (int i = 0; i < listSkill.Count; i++)
        {
            listSkill[i].time *= 3 / 2;
        }
    }

    private void NextAction()
    {
        currentAction += 1;
        if (currentAction >= listSkill.Count)
        {
            currentAction = 0;
        }
    }

    public void HandleHurt(int damage)
    {
        currentHP -= damage;
    }
}
