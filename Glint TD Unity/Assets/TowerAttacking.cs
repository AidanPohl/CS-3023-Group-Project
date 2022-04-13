/**
 * Created By: Aidan Pohl
 * Date Created: April 4, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 10, 2022
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

    
    [Header("Attack Stats")]
    public TargetingTypes targeting;
    public float range = 0f; //How far it can see/attack
    public AttackTypes attackType = AttackTypes.Projectile;
    public enum AttackTypes {Projectile, Beam, Pulsar, None}
     public float projectileSpeed = 1;
    public GameObject attackPrefab;
    public enum TargetingTypes {First, Last, Close, Null}
    private Transform targetProtect;
    public Transform target;
    public HashSet<Transform> enemiesInRange;

    // Start is called before the first frame update
    private void Awake()
    {
        footprintCollider = GetComponent<SphereCollider>();
        footprintCollider.radius = footprint;
        
    }
    void Start()
    {
        Targeting();
        InvokeRepeating("Fire", 0f, 1/attacksPerSecond); //finds new target and fires
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if( target == null) { return; } //if not target do not fire

            GameObject projGO = Instantiate(attackPrefab,transform); //Creates a new Projectile
            Vector3 toEnemy = target.position - transform.position; // Gets the direction between tower and target
            toEnemy.Normalize();

            projGO.transform.position = transform.position;
            Rigidbody rb = projGO.GetComponent<Rigidbody>(); //Send the projectile to fire at the target
            projGO.transform.LookAt(target);//point projectile at target
            rb.velocity = toEnemy * projectileSpeed;//send projectile at speed towards current target position
    }

    private Transform UpdateTarget()
    {   
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
