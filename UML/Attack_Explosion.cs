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
    private Material bombMat;   //explosion material
    private Color bombColor;    //explositon initial color
    private Color bombColorOpaque;//obpaque version
    public AudioClip explosionSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake(){
        bombMat = gameObject.GetComponent<Renderer>().material;
        bombColor = bombMat.color;
        transform.localScale = Vector3.one*.01f;
        bombColorOpaque = bombColor;
        bombColorOpaque.a =1;
        audioSource = GetComponent<AudioSource>();
    }//ed Awake()

    // Update is called once per frame
    override protected void Update()
    {   
        if(exploding){
        transform.localScale = Vector3.one*Mathf.Lerp(transform.localScale.x,radius,5*Time.deltaTime); //expands explosion
        Color curColor = bombMat.color; ///unfades bomb
        curColor.a += .01f;
        bombMat.color = Color.Lerp(bombColor,bombColorOpaque,transform.localScale.x/radius);
        if(transform.lossyScale.x > radius-.05f){//if bomb larger than radius, deactivate it
            Debug.Log("Max Sized Reached");
            exploding = false;
            gameObject.SetActive(false);
        }
        }
    }//end Update()

    public void Boom(){
        exploding = true;
        bombMat.color = bombColor;
        transform.localScale = Vector3.one*.01f;
        if(audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);//play the projectile sound
        }
    }//end Boom()

    public override int EnemyCollision(){
        return damage;
    }
}
