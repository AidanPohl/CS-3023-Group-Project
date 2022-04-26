/***
*
*  Made By : Xingzhou Li
*  Created : April 25, 2022
*  
*
*   Last Edited By: Xingzhou Li
*   Last Edited: April 25, 2022

//used to spawn enemy
***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Wave Setting")]
    public GameObject[] enemys;
    [SerializeField] private float time = 2f; // time per wave 
    [SerializeField] private float times = 1f; // time between enemy spawn in one wave
    [SerializeField] private float count = 5f; // wave numbers
    [SerializeField] private float counts = 4f; // enemies in each wave


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateEnemy()
    {
        for (int i = 0; i < count; i++)
        {
            for(int j = 0; j < counts; j++)
            {
                Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position, Quaternion.identity); // Spawn enemy randomly
                yield return new WaitForSeconds(times);
            } // end for
            yield return new WaitForSeconds(time);
        } // end for
    }
}
