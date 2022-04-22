/**
 * Created By: Aidan Pohl
 * Date Created: April 22, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 22, 2022
 * 
 * Description: Bomb Explosion
 * */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Explosion : Attack_Pulse
{
    public float radius;
    public bool exploding;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {   
        if(exploding){
        transform.localScale *= 1+(radius*Time.deltaTime);
        if(transform.lossyScale.x > radius){
            exploding = false;
            gameObject.SetActive(false);
        }
        }
    }
    public void Boom(){
        exploding = true;
    }
}
