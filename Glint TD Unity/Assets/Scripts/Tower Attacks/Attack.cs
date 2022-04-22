/**
 * Created By: Aidan Pohl
 * Date Created: April 21, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 21, 2022
 * 
 * Description: Generic Attack
 * */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{   
    public int damage;
    
    public virtual int EnemyCollision(){
        gameObject.SetActive(false);
        return damage;
    }
}
