/***
 * Created By: Aidan Pohl
 * Created: April 10, 2022
 * 
 * Last Edited by: April 18,2022
 * 
 * Description: Return Object to Pool
 * 
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{
    public GameObject poolGO;
    public ObjectPool pool;
    private void Start()
    {
        pool = poolGO.GetComponent<ObjectPool>().POOL;
    }

    private void OnDisable()
    {
        if (pool != null)
        {   
            transform.position = poolGO.transform.position;
            pool.ReturnObject(this.gameObject); //return this object to pool
        }
    }

    public void Return()
    {
        transform.position = poolGO.transform.position;
        pool.ReturnObject(this.gameObject); //return this object to pool
        gameObject.SetActive(false);
    }
}
