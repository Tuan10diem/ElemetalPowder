using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Subjects
{

    public List<GameObject> enemyList;
    public List<GameObject> playerList;

    // Update is called once per frame
    void Update()
    {
        bool enemyRemaining = false;
        bool playerRemaining = false;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i]) 
            { 
                enemyRemaining = true;
                break;
            }
        }
        for(int i = 0;i < playerList.Count; i++)
        {
            if (playerList[i]) 
            { 
                playerRemaining = true; 
                break;
            }
        }
        if(!enemyRemaining && playerRemaining)
        {
            NotifyObservers(PlayerAction.Win, 0);
            enemyList.Clear();
            playerList.Clear();
        }
        if(!playerRemaining && enemyRemaining)
        {
            NotifyObservers(PlayerAction.Lose, 0);
            enemyList.Clear();
            playerList.Clear();
        }
    }
}
