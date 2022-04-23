/***
*
*  Made By : Xingzhou
*  Created : April 20, 2022
*  
*
*   Last Edited By: Aidan Pohl
*   Last Edited: April 22, 2022
***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 10f;
    public int score = 100;
    public float health = 10;

    public Transform[] waypoints;


    private Transform target;
    private int wavepointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];

    }

    // Update is called once per frame
    void Update()
    {   
        if(health <= 0){
            Die();
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= waypoints.Length-1)//check if reached the end of the array
        {   
            Destroy(gameObject);
        }else
        {
        wavepointIndex++;
        target = waypoints[wavepointIndex];
        }
    }

    void Die(){

    }


    void OnCollisionEnter(Collision other){ ///AP
        if (other.gameObject.tag == "Projectile"){
            health -= other.gameObject.GetComponent<Attack>().EnemyCollision();
        }
    }
}
