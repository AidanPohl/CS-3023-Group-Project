/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 24, 2022
*
* Description: terrain tile manager
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{   
    public Material pathMat;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Waypoint"){
            gameObject.tag = "Path Tile";
            GetComponent<Renderer>().material = pathMat;
        }
    }

}
