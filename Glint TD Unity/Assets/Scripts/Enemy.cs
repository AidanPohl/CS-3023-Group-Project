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
    private GameManager gm;
    public float speed = 10f;
    public int score = 100;
    public float health = 10;
    public GameObject waypointsGO;
    public List<GameObject> waypoints;
    public bool devFrozen;          //DEVTOOL:Does not move or die
    private Transform target;
    private int wavepointIndex = 1;

    void Awake(){
        gm = GameManager.GM;
        if(!waypointsGO){
            waypointsGO = GameObject.Find("Waypoints");
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {   waypoints = waypointsGO.GetComponent<Waypoints>().waypoints;

        target = waypoints[wavepointIndex].transform;
    }//end Start

    // Update is called once per frame
    void Update()
    {   if(!devFrozen){
            if(health <= 0){
                Die();
            }else{
                transform.position = Vector3.MoveTowards(transform.position,target.position,speed * Time.deltaTime);

                if(transform.position == target.position){
                    GetNextWaypoint();
                }//end if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            }//end if (health<=0) else
        }//end if (!devFrozen)
    }//end Update()

    void GetNextWaypoint()
    {
        if (wavepointIndex >= waypoints.Count-1) //check if reached the end of the array
        {   
            //gm.lives -= (int)health;
            Destroy(gameObject);
        }else
        {
        wavepointIndex++;
        target = waypoints[wavepointIndex].transform;
        }
    }

    void Die(){

    }


    void OnCollisionEnter(Collision other){ ///AP
        Debug.Log("Collision!");
        if (other.gameObject.tag == "Tower Projectile"){
            health -= other.gameObject.GetComponent<Attack>().EnemyCollision();
        }
    }
}
