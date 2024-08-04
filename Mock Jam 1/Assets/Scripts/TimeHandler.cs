using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public TMP_Text timeText;
    private float currentTime;
    private GameObject player;
    public EnemySpawner enemySpawner;
    public int difficultyLevel = 1;
    
    void Start()
    {
        InvokeRepeating("Timer", 0.1f, Time.deltaTime);
        player = GameObject.FindGameObjectWithTag("Player");
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        //timeText.text = currentTime.ToString("F2");
        if (player.GetComponent<PlayerScript>().getPlayerState() == PlayerScript.PlayerState.dead) {
            CancelInvoke("Timer");
        }
    }

    public void Timer()
    {
        Manager managerVar = Manager.GetInstance();
        var timeVar = managerVar.totalTime;
        currentTime = timeVar;

        var t0 = (int) currentTime;
        var m = t0/60;
        var s = t0 - m*60;
        var ms = (int)((currentTime - t0)*100);
        timeText.text = $"{m:00}:{s:00}:{ms:00}";
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty() {
        switch (difficultyLevel) {
            case 1: 
            if(currentTime >= 30f) {
                difficultyLevel++;
                Debug.Log("Difficulty Increase");
                enemySpawner.setSpawnDelay(2);
            }
            break;
            case 2:
            if (currentTime >= 60f){
                difficultyLevel++;
                player.GetComponent<PlayerScript>().moveSpeed = 7f;
            }
            break;
            case 3:
            if (currentTime >= 90f) {
                difficultyLevel++;
                enemySpawner.setSpawnDelay(1);
            }
            break;
        }
    }
}   
