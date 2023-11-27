using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Subjects
{

    public List<GameObject> enemyList;
    

    public CircleCollider2D collider2D;
    public AnimatedSpriteRenderer spriteRenderer;

    private void Start()
    {
        InvokeRepeating("CheckState", 0f, 0.1f) ;
    }

    // Update is called once per frame
    private void CheckState()
    {
        bool isWin = true;
        Debug.Log("running" + isWin.ToString());
        for (int i = 0; i < enemyList.Count; i++)
        {
            Debug.Log(enemyList[i].activeSelf);
            if (enemyList[i].activeSelf)
            {
                isWin = false;
            }
        }
        if(isWin)
        {
            Debug.Log("Win");
            collider2D.enabled = true;
            spriteRenderer.enabled = true;
            NotifyObservers(PlayerAction.Win, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision) return;
        if (collision.CompareTag("Player") )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
