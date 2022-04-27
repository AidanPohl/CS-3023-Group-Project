/**
 * Created By: Aidan Pohl
 * Date Created: April 22, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 22, 2022
 * 
 * Description: Bomb Attack
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bomb : Attack
{   
    public string explosionPoolName = "Explosion Pool";
    private ObjectPool ExplosionPool;

    //Awake
    void Awake(){
        ExplosionPool = GameObject.Find(explosionPoolName).GetComponent<ObjectPool>();
    }

    public override int EnemyCollision(){
        GameObject GOExplosion = ExplosionPool.GetObject();
        GOExplosion.transform.position = transform.position;
        GOExplosion.GetComponent<Attack_Explosion>().Boom();
        GOExplosion.SetActive(true);
        gameObject.SetActive(false);
        return damage;
    }


}
