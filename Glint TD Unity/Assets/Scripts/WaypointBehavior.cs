/***
*
*  Made By : Jason Alfrey
*  Created : April 17, 2022
*  
*
*   Last Edited By: Jason Alfrey
*   Last Edited: April 16, 2022
* 
*   Description: Used to implement the waypoint behavior for the project. 
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{
    public static Transform[] pointsArray;
    private void Awake()
    {
        pointsArray = new Transform[transform.childCount];
        for(int i = 0; i < pointsArray.Length; i++)
        {
            pointsArray[i] = transform.GetChild(i);
        }
    }
}
