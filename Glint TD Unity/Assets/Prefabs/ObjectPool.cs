/***
 * Created By: Aidan Pohl
 * Created: April 10, 2022
 * 
 * Last Edited by: April 18, 2022
 * 
 * Description: Object Pool for objects
 * 
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public ObjectPool POOL;
    private Queue<GameObject> objects = new Queue<GameObject>(); //queue of game objects to be added to the pool

    [Header("Pool Settings")]
    public GameObject objectPrefab;
    public int poolStartSize = 5;
    private void Awake()
    {
        POOL = this;
    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < poolStartSize; i++)
        {
            GameObject gObject = Instantiate(objectPrefab,transform);
            gObject.GetComponent<PoolReturn>().poolGO = gameObject;
            objects.Enqueue(gObject); //add to queue
            gObject.SetActive(false);
        }//end for loop
    }//end Start()

    public GameObject GetObject()
    {
        if(objects.Count > 0)
        {
            GameObject gObject = objects.Dequeue();
            gObject.SetActive(true);
            return gObject;
        }
        else {
            GameObject gObject = Instantiate(objectPrefab,transform); //make a new object if queue is empty
            gObject.GetComponent<PoolReturn>().poolGO = gameObject;
            gObject.SetActive(true);
            return gObject; 
        }//end if else
    }//end getObject()

    public void ReturnObject(GameObject gObject)
    {
        objects.Enqueue(gObject);
        gObject.SetActive(false);
    }//end ReturnObject(GameObject)
}