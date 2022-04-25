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
    public float enemySpeed = 5f;
    public float enemyHealth = 10f;
    public float spawnInteval = 2f;
    public List<GameObject> prefabs;


    public Stopwatch timer;
    public GameObject enemy;
    public Transform start;
    private int initialIndex; 
    



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
    }

    IEnumerator SpawnDelay()
    {
        sEnemy();
        yield return new WaitForSeconds(spawnInteval);
        StartCoroutine(SpawnDelay());

    }


    void sEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemy);
        spawnedEnemy.transform.position = start.position;
    }

}
