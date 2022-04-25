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
    public bool active;          //DEVTOOL:Does not move or die
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
    {   if(active){
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
<<<<<<< Updated upstream:Glint TD Unity/Assets/Scripts/Enemy.cs
            //gm.lives -= (int)health;
            Destroy(gameObject);
=======
            gm.SubLives(health);
            gm.money += health;
            gameObject.SetActive(false);
>>>>>>> Stashed changes:Glint TD Unity/Assets/Scripts/Enemies/Enemy.cs
        }else
        {
        wavepointIndex++;
        target = waypoints[wavepointIndex].transform;
        }
    }

<<<<<<< Updated upstream:Glint TD Unity/Assets/Scripts/Enemy.cs
    void Die(){

=======
    void Die(){//kills enemy AP
            gm.score += score;
            gm.money += maxHealth;
            target = null;
            active = false;
            gameObject.GetComponent<PoolReturn>().Return();
    }

    public void Renew(int health, float speed, int score){ //refreshes enemy stats AP
        this.health = health;
        maxHealth = health;
        this.speed = speed;
        this.score = score;
        waypointIndex = 1;
        waypoints = waypointsGO.GetComponent<Waypoints>().waypoints;
        target = waypoints[1].transform;
        gameObject.GetComponent<Renderer>().material.color = beginColor;
        active = true;
>>>>>>> Stashed changes:Glint TD Unity/Assets/Scripts/Enemies/Enemy.cs
    }


    void OnCollisionEnter(Collision other){ ///AP
        Debug.Log("Collision!");
        if (other.gameObject.tag == "Tower Projectile"){
            health -= other.gameObject.GetComponent<Attack>().EnemyCollision();
        }
    }
}
