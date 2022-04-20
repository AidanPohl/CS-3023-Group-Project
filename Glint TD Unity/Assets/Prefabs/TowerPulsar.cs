/**
 * Created By: Aidan Pohl
 * Date Created: April 20, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 20, 2022
 * 
 8 Description: Funny object do the shooty shoot
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class TowerPulsar : MonoBehaviour
{
    /**VARIABLES**/
    [Header("Tower Stats")]

    public float footprint = 1;
    public float attacksPerSecond = 1; //How quickly it attacks
    public int attackStrength = 1; //How much damage each attack does
    private SphereCollider footprintCollider;
    private Transform transTurret;
    private Transform transBase;
    [Header("Attack Stats")]
    public float range = 0f; //How far it can see/attack
    public float pulseSpeed = 1;
    private GameObject pulsar;
    public HashSet<Transform> enemiesInRange;

    // Start is called before the first frame update
    private void Awake()
    {
        footprintCollider = GetComponent<SphereCollider>();
        footprintCollider.radius = footprint;
        enemiesInRange = new HashSet<Transform>();
        transTurret = transform.Find("Turret");
        transBase = transform.Find("Base");
        pulsar = transform.Find("Tower pulsar").gameObject;

    }

    void Start()
    {
        Activate();
    }
    void Activate()
    {
        InvokeRepeating("Pulse", 0f, 1/attacksPerSecond); //fires
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion tureetRot = transTurret.rotation;
        Vector3 rot = tureetRot.eulerAngles;
        rot.y += .1f;
        tureetRot.eulerAngles = rot;
        transTurret.rotation = tureetRot;
    }



    private void Pulse()
    {
        if (enemiesInRange.Count > 0) {
            pulsar.GetComponent<SphereCollider>();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ping");
        GameObject collGO = collision.gameObject;
        if (collGO.tag == "Enemy")
        {
            enemiesInRange.Add(collGO.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
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
