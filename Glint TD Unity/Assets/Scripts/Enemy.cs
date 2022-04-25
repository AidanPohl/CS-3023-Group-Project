/***
*
*  Made By : Xingzhou
*  Created : April 20, 2022
*  
*
*   Last Edited By: Aidan Pohl
*   Last Edited: April 24, 2022
***/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   public GameManager gm;

    [Header("Enemy Settings")]
    public float speed = 10f;
    public int score = 100;
    public int health = 10;
    public GameObject waypointsGO;
    public List<GameObject> waypoints;
    public bool devFrozen;          //DEVTOOL:Does not move or die
    private Transform target;
    public int waypointIndex = 1;

    void Awake(){
  
        if(!waypointsGO){
            waypointsGO = GameObject.Find("Waypoints");
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {   gm = GameManager.GM;
        waypoints = waypointsGO.GetComponent<Waypoints>().waypoints;

        target = waypoints[waypointIndex].transform;
    }//end Start

    // Update is called once per frame
    void Update()
    {   if(!devFrozen){
            if(health <= 0){
                Die();
            }else{
            
                transform.position = Vector3.MoveTowards(transform.position,target.position,speed * Time.deltaTime);

                if(Vector3.Distance(transform.position,target.position)<.01f){
                    GetNextWaypoint();
                
                }//end if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            }//end if (health<=0) else
        }//end if (!devFrozen)
    }//end Update()

    void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Count-1) //check if reached the end of the array
        {   
            gm.SubLives(health);

            gameObject.SetActive(false);
        }else
        {
        waypointIndex++;
        target = waypoints[waypointIndex].transform;
        }
    }

    void Die(){
            gm.score += score;
            gm.money += score/10;
            gameObject.SetActive(false);
            target = null;
    }

    public void Renew(int health, float speed, int score){
        this.health = health;
        this.speed = speed;
        this.score = score;
        waypointIndex = 1;
        waypoints = waypointsGO.GetComponent<Waypoints>().waypoints;
        target = waypoints[1].transform;
    }

    void OnCollisionEnter(Collision other){ ///AP
        Debug.Log("Collision!");
        if (other.gameObject.tag == "Tower Projectile"){
            health -= other.gameObject.GetComponent<Attack>().EnemyCollision();
        }
    }
}
