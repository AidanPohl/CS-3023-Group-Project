/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 24, 2022
*
* Description: Waypoint Generation and array holder.
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{   
    /***VARIABLES**/
    [Header("Set In Inspector")]
    public GameObject[] vertices; //the corners of the enemy path
    public GameObject waypointPrefab; //prefab for waypoint
    public int waypointSeperation = 1; //how close waypoints are set to each other
    [Header("Set in Script")]
    public List<GameObject> waypoints; //the list of waypoints
    // Start is called before the first frame update
    void Awake()
    {
        waypoints = new List<GameObject>(); //creates new array for waypoint
        Vector3 curVerPos;
        Vector3 nxtVerPos;
        float vertexDist;
        int waypointNum = 1;
        for(int i = 0; i < (vertices.Length-1); i++){//outer loop for each run between vertices
            waypoints.Add(vertices[i]); //add current vertex
            curVerPos = vertices[i].transform.position;                  //position of current vertex
            nxtVerPos = vertices[i+1].transform.position;               //position of next vertex
            vertexDist = Vector3.Distance(curVerPos,nxtVerPos);         //distance btween current and next vertex
            Debug.Log(vertexDist);
            float numWaypoints = Mathf.Round((vertexDist/waypointSeperation))-1;  //number of waypoints to add between vertices
            Debug.Log(numWaypoints);
            for(int j = 1; j <= numWaypoints; j++){//inner loop for each waypoint
                GameObject newWaypoint = Instantiate(waypointPrefab,transform); //instantiate new waypoint
                Debug.Log(((j/(numWaypoints+1))*vertexDist));
                newWaypoint.transform.position = Vector3.MoveTowards(curVerPos,nxtVerPos,((j/(numWaypoints+1))*vertexDist));   //set position of new waypoint
                newWaypoint.name = "Waypoint "+waypointNum;
                waypointNum++;
                waypoints.Add(newWaypoint);
                Debug.Log(newWaypoint.name);
            }//end innter for looop
        }//end outer for loop
        waypoints.Add(vertices[vertices.Length-1]);

    }//end start

    // Update is called once per frame
    void Update()
    {
        
    }
}
