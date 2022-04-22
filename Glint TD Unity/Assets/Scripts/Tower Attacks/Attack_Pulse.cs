/**
 * Created By: Aidan Pohl
 * Date Created: April 22, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 22, 2022
 * 
 * Description: Pulse Attack
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Pulse : Attack
{
    public override int EnemyCollision(){
        return damage;
    }
}
