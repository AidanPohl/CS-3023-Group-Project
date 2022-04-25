<<<<<<< Updated upstream:Glint TD Unity/Assets/Scripts/SpawnEnemy.cs
=======
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
using System;
>>>>>>> Stashed changes:Glint TD Unity/Assets/Scripts/Enemies/SpawnEnemy.cs
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
<<<<<<< Updated upstream:Glint TD Unity/Assets/Scripts/SpawnEnemy.cs
{
    public float enemySpeed = 5f;
    public float enemyHealth = 10f;
    private Transform dest;
=======
{   GameManager gm;
    public float enemySpeed = 1f;
    public int enemyHealth = 1;
    public int enemyScore = 10;
    public float spawnInteval = 2f;
    public double secsPassed;
    public double difficulty;
    public Stopwatch timer;
    public GameObject enemyPoolGO;
    public Transform start;
>>>>>>> Stashed changes:Glint TD Unity/Assets/Scripts/Enemies/SpawnEnemy.cs
    private int initialIndex; 
    

    // Start is called before the first frame update
    void Start()
    {
        dest = WaypointBehavior.pointsArray[0];
    }

<<<<<<< Updated upstream:Glint TD Unity/Assets/Scripts/SpawnEnemy.cs
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = dest.position - transform.position; 
        //transform.Translate 
    }
=======
    void Update(){
        TimeSpan curTime = GameManager.timer.Elapsed.Duration();
        secsPassed = curTime.TotalSeconds;
        difficulty = secsPassed/100;
    }
    IEnumerator SpawnDelay()
    {
        EnemySpawn();
        yield return new WaitForSeconds(Mathf.Pow(spawnInteval,(float)(1-(difficulty))));
        StartCoroutine(SpawnDelay());

    }


    void EnemySpawn()
    {
        GameObject spawnedEnemy = enemyPool.GetObject();
        spawnedEnemy.transform.position = start.position;
        spawnedEnemy.GetComponent<Enemy>().Renew(enemyHealth+(int)(difficulty*10),enemySpeed+(float)difficulty,enemyScore+(int)(difficulty*100));
    }

>>>>>>> Stashed changes:Glint TD Unity/Assets/Scripts/Enemies/SpawnEnemy.cs
}
