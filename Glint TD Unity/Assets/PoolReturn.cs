/***
 * Created By: Aidan Pohl
 * Created: April 10, 2022
 * 
 * Last Edited by: N?/A
 * 
 * Description: Return Object to Pool
 * 
 * 
 ***/using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{
    public GameObject poolGO;
    ObjectPool pool;
    private void Awake()
    {
        pool = poolGO.GetComponent<ObjectPool>().POOL;
    }

    private void OnDisable()
    {
        if (pool != null)
        {
            pool.ReturnObject(this.gameObject); //return this object to pool
        }
    }
}
