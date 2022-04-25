/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 24, 2022
*
* Description: terrain generation
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{   
    public GameObject terrainTilePrefab;
    public Vector2 mapSize;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < mapSize.x; x++){
            for(int y = 0; y < mapSize.y; y++){
                GameObject newTile = Instantiate(terrainTilePrefab, transform);
                newTile.transform.localPosition = new Vector3(x-(mapSize.x/2), 0, y-(mapSize.y/2));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
