/**
 * Created By: Aidan Pohl
 * Date Created: April 21, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 24, 2022
 * 
 * Description: Generic Tower
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
[SelectionBase]
public class Tower : MonoBehaviour
{
    /**VARIABLES**/
    [Header("Tower Stats")]
    protected bool active = false;
    public bool devActivate = false;

    protected SphereCollider rangeCollider;
    protected Transform transTurret;
    protected Transform transBase;
    public List<Transform> enemiesInRange = new List<Transform>();
    public float range = 0f; //How far it can see/attack

    public bool placeable;
 

    

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = range;
        enemiesInRange = new List<Transform>();
        transTurret = transform.Find("Turret");
        transBase = transform.Find("Base");
    }

    virtual public void Activate()
    {   
        Debug.Log(gameObject.name +" activated.");
        active = true;
        devActivate = true;
    }
    virtual public void Deactivate(){
        Debug.Log(gameObject.name +" deactivated.");
        active = false;
        devActivate=false;
        CancelInvoke();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if(devActivate && !active){
            Activate();
        }else if(!devActivate && active){
            Deactivate();
        }
    }

    protected void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ping");
        GameObject collGO = collision.gameObject;
        if (collGO.tag == "Enemy")
        {
            Debug.Log(collision);
            enemiesInRange.Add(collision.transform);
        }
    }

    protected void OnTriggerExit(Collider collision)
    {
        GameObject collGO = collision.gameObject;
        if (collGO.tag == "Enemy")
        {
            enemiesInRange.Remove(collGO.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
