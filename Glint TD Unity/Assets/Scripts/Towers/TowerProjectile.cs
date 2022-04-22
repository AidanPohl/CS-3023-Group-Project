/**
 * Created By: Aidan Pohl
 * Date Created: April 4, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 21, 2022
 * 
 8 Description: Funny object do the shooty shoot
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class TowerProjectile : Tower
{
    /**VARIABLES**/
    [Header("Attack Stats")]
    public float attacksPerSecond = 1; //How quickly it attacks
    public int attackStrength = 1; //How much damage each attack does
    public enum TargetingTypes {First, Last, Close, Null}
    private TargetingTypes targeting = TargetingTypes.Close;
    public float projectileSpeed = 1;
    public string attackPoolName = "AttackPool1";
    private ObjectPool attackPool;

    private Transform targetProtect;
    private Transform target = null;

    // Start is called before the first frame update
    new private void Awake()
    {
        base.Awake();
        attackPool = GameObject.Find(attackPoolName).GetComponent<ObjectPool>();
    }

    new void Activate()
    {   
        base.Activate();
        Targeting();
        InvokeRepeating("Fire", 0f, 1/attacksPerSecond); //finds new target and fires
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (target)
        {
            transTurret.LookAt(target);
        }
    }

    public void Fire() {
       target = UpdateTarget();
       ProjectileFire();
    }

    private void ProjectileFire()
    {
        if (!target)
        { //if no target do not fire
            return;
        }
        else {
            Debug.Log(target);
            GameObject projGO = attackPool.POOL.GetObject(); //Creates a new Projectile
            projGO.GetComponent<Attack>().damage = attackStrength; //Set projectile damage
            Vector3 toEnemy = target.position - transTurret.position; // Gets the direction between tower and target
            toEnemy.Normalize();

            projGO.transform.position = transTurret.position;
            Rigidbody rb = projGO.GetComponent<Rigidbody>(); //Send the projectile to fire at the target
            projGO.transform.LookAt(target);//point projectile at target
            rb.velocity = toEnemy * projectileSpeed;//send projectile at speed towards current target position
            projGO.SetActive(true);
        }
    }

    private Transform UpdateTarget()
    {
        Debug.Log(enemiesInRange.Count);
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

}
