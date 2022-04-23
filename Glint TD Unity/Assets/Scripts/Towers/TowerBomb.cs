/**
 * Created By: Aidan Pohl
 * Date Created: April 23, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 23, 2022
 * 
 * Description: Bomb Tower woo!
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBomb : TowerProjectile
{   
    public Transform transCannon;

    override protected void Awake(){
        base.Awake();
        transCannon = transform.Find("Cannon");
        fireFrom = transCannon.position;                //fires from cannon instead of turret;
    }//end Awake();

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if(target){
            Vector3 turretRot = transTurret.rotation.eulerAngles;
            turretRot.x = 0;
            turretRot.z = 0;
            transTurret.rotation = Quaternion.Euler(turretRot);  //Removes X and Z rotation from turret (legs)

            transCannon.LookAt(target);                          //faces cannon at target
        }
    }
}
