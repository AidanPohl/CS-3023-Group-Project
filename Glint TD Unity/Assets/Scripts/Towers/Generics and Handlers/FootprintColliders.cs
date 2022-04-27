/**
 * Created By: Aidan Pohl
 * Date Created: April 24, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 24, 2022
 * 
 * Description: Check for Collisions with other towers or path blocks
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintColliders : MonoBehaviour
{   
    private Tower towerScript;
    private HashSet<Transform> invalids;

    void Awake(){
        towerScript = transform.parent.GetComponent<Tower>();
        invalids = new HashSet<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invalids.Count == 0){
            towerScript.placeable = true;
        } else{
            towerScript.placeable = false;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Footprint" || other.gameObject.tag == "Path Tile"){
            invalids.Add(other.transform);
        }
    }

        void OnTriggerExit(Collider other){
        if(other.gameObject.name == "Footprint" || other.gameObject.tag == "Path Tile"){
            invalids.Remove(other.transform);
        }
    }
}
