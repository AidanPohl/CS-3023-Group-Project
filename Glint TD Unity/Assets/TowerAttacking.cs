/**
 * Created By: Aidan Pohl
 * Date Created: April 4, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 18, 2022
 * 
 8 Description: Funny object do the shooty shoot
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class TowerAttacking : MonoBehaviour
{
    /**VARIABLES**/
    [Header("Tower Stats")]

    public float footprint = 1;
    public float attacksPerSecond = 1; //How quickly it attacks
    public int attackStrength = 1; //How much damage each attack does
    private SphereCollider footprintCollider;
    private GameObject gOTurret;
    private GameObject gOBase;
    [Header("Attack Stats")]
    public TargetingTypes targeting;
    public float range = 0f; //How far it can see/attack
    public AttackTypes attackType = AttackTypes.Projectile;
    public enum AttackTypes {Projectile, Beam, Pulsar, None}
    public float projectileSpeed = 1;
    public string attackPoolName = "AttackPool1";
    private ObjectPool attackPool;
    public enum TargetingTypes {First, Last, Close, Null}
    private Transform targetProtect;
    private Transform target = null;
    public HashSet<Transform> enemiesInRange;

    // Start is called before the first frame update
    private void Awake()
    {
        footprintCollider = GetComponent<SphereCollider>();
        footprintCollider.radius = footprint;
        enemiesInRange = new HashSet<Transform>();
        attackPool = GameObject.Find(attackPoolName).GetComponent<ObjectPool>();
    }

    void Activate()
    {
        Targeting();
        InvokeRepeating("Fire", 0f, 1/attacksPerSecond); //finds new target and fires
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            gOTurret.transform.LookAt(target);
        }
    }

    public void Fire() {
        UpdateTarget();
            switch (attackType)
            {
                case AttackTypes.Projectile:
                    ProjectileFire();
                    break;
            }
    }

    private void ProjectileFire()
    {
        if (!target) { //if no target do not fire
            GameObject projGO = attackPool.POOL.GetObject(); //Creates a new Projectile
            Vector3 toEnemy = target.position - transform.position; // Gets the direction between tower and target
            toEnemy.Normalize();

            projGO.transform.position = transform.position;
            Rigidbody rb = projGO.GetComponent<Rigidbody>(); //Send the projectile to fire at the target
            projGO.transform.LookAt(target);//point projectile at target
            rb.velocity = toEnemy * projectileSpeed;//send projectile at speed towards current target position
            projGO.SetActive(true);
        }
    }

    private Transform UpdateTarget()
    {   
        if (enemiesInRange.Count == 0)
        {
            return null;
        }
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemiesInRange)
        {
            float distToEnemy = Vector3.Distance(targetProtect.position, enemy.position);
            if (distToEnemy < minDistance)
            {
                minDistance = distToEnemy;
                closestEnemy = enemy;
            }

        }
        return closestEnemy;
    }

    private void Targeting()
    {
        switch (targeting)
        {
            case TargetingTypes.Close:
                targetProtect = transform;
                break;
        }
    }//end Targeting()

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
