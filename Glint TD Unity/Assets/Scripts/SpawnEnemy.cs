/***
*
*  Made By : Garron Denney
*  Created : April 24, 2022
*  
*
*   Last Edited By: Aidan Pohl
*   Last Edited: April 25, 2022

//used for implementing a survival based enemy deployment system
***/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class SpawnEnemy : MonoBehaviour
{   GameManager gm;
    public float enemySpeed = 1f;
    public int enemyHealth = 1;
    public int enemyScore = 10;
    public float spawnInteval = 2f;

    public float difficulty;
    public Stopwatch timer;
    public GameObject enemyPoolGO;
    public Transform start;
    private int initialIndex; 
    private ObjectPool enemyPool;



    public void StartSpawning()
    {
        //call spawn handler
        StartCoroutine(SpawnDelay());

    }
    // Start is called before the first frame update
    void Start()
    {
       gm = GameManager.GM;
       timer = GameManager.timer;
       enemyPool = enemyPoolGO.GetComponent<ObjectPool>();
    }

    void Update(){
        difficulty = (float)timer.Elapsed.Duration().TotalSeconds/100;
    }
    IEnumerator SpawnDelay()
    {
        sEnemy();
        yield return new WaitForSeconds(Mathf.Pow(spawnInteval,(1-difficulty)));
        StartCoroutine(SpawnDelay());

    }


    void sEnemy()
    {
        GameObject spawnedEnemy = enemyPool.GetObject();
        spawnedEnemy.transform.position = start.position;
        spawnedEnemy.GetComponent<Enemy>().Renew(enemyHealth,enemySpeed,enemyScore+(int)timer.Elapsed.Duration().TotalSeconds);
    }

}
