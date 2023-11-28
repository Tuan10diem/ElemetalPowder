using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Subjects
{

    public List<GameObject> enemyList;
    public new CircleCollider2D collider2D;
    public AnimatedSpriteRenderer spriteRenderer;

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1.0f;
    }

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
            NotifyObservers(PlayerAction.Win, 0);
            collider2D.enabled = true;
            spriteRenderer.enabled = true;
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
