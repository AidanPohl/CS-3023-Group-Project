/**
 * Created By: Aidan Pohl
 * Date Created: April 4, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 25, 2022
 * 
 * Description: Funny object do the shooty shoot
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class TowerProjectile : Tower
{
    /**VARIABLES**/
    [Header("Attack Stats")]
    public float attacksPerSecond = 1; //How quickly it attacks
    public int attackStrength = 1; //How much damage each attack does
    public enum TargetingTypes {First, Last, Close, Null}
    protected TargetingTypes targeting = TargetingTypes.Close;
    public float projectileSpeed = 1;//how fast the projectiles move
    public string attackPoolName;//name of the projectile object pool
    public ObjectPool attackPool;//where it gets its projectiles from
    public AudioClip projectileSound;
    AudioSource audioSource;

    protected Transform targetProtect;//what the tower is protecting (decides what to target)
    protected Transform target = null;//target to fire at
    protected Transform fireFrom;//where the projectiles launch from

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        audioSource = gameObject.GetComponent<AudioSource>();
        try{
        attackPool = GameObject.Find(attackPoolName).GetComponent<ObjectPool>(); // gets Attack pool
        } catch(Exception e){
            Debug.LogError(e);
        }
        fireFrom = transTurret;
    }//end Awake();

    override public void Activate()
    {   
        base.Activate();

        if(!attackPool){ //checks for attack pool
            Debug.LogError(gameObject.name+" has no Attack Pool!");
            Deactivate();
            return;
        }

        Targeting(); //chooses targeting type (NOT IMPLIMENTED)
        InvokeRepeating("Fire", 0.5f, 1/attacksPerSecond); //finds new target and fires every 1/attackspersecond seconds
    }//end Activate();


    virtual protected void LateUpdate(){
        if (target && target.gameObject.active) //Point turret towards target
        {
            transTurret.LookAt(target);
        }//end if(target)
    }

    private void Fire() //Fires a projectile at the current target
    {  target = UpdateTarget();                                             //get new target

        if (!target || !target.gameObject.active)
        { //if no target do not fire
            return;
        }
        else {
            Debug.Log(gameObject.name + ": Firing at "+target.gameObject.name);
            Debug.Log(target);
            GameObject projGO = attackPool.POOL.GetObject();                //Gets a new Projectile
            projGO.transform.position = fireFrom.position;                           //sets projectile initial position
            projGO.GetComponent<Attack>().damage = attackStrength;          //Set projectile damage
            Vector3 toEnemy = target.position - projGO.transform.position;  // Gets the direction between tower and target
            toEnemy.Normalize();                                            //normalizes direction vector


            Rigidbody rb = projGO.GetComponent<Rigidbody>();                //Send the projectile to fire at the target
            projGO.transform.LookAt(target);                                //point projectile at target
            rb.velocity = toEnemy * projectileSpeed;                        //send projectile at speed towards current target position
            projGO.SetActive(true);

            if(audioSource != null && projectileSound != null)
            {
                audioSource.PlayOneShot(projectileSound);//play the projectile sound
            }
        }//end if else
    }//end ProjectileFire()

    private Transform UpdateTarget() //updates the current target
    {
        if (enemiesInRange.Count == 0) //if no enemies in range, no target
        {
            return null;
        }
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemiesInRange) //checks which enemy is closest to targetprotect
        {      if(enemy.gameObject.active){
            Debug.Log(gameObject.name+":  Checking "+enemy.gameObject.name+" position...");
            float distToEnemy = Vector3.Distance(targetProtect.position, enemy.position);
            if (distToEnemy < minDistance)
            {
                minDistance = distToEnemy;
                closestEnemy = enemy;
            }
        }
        }
        return closestEnemy;
    }//end UpdateTarget()

    private void Targeting() //choses target protect based on targting type (WIP)
    {   
        Debug.Log(gameObject.name+": Setting Targeting Method.");
        switch (targeting)
        {
            case TargetingTypes.Close:
                targetProtect = transform;
                break;
            //TODO: Impliment other TargetingTypes (First, Last, Strong)
        }
    }//end Targeting()

}//end TowerProjectile
