/**
 * Created By: Aidan Pohl
 * Date Created: April 22, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 23, 2022
 * 
 * Description: Generic Tower
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class TowerLaser : Tower
{   
    public Transform target;
    public float attacksPerSecond = 1;
    public int damage;
    public LineRenderer laserLine;
    private Vector3 startPos;
    private Color laserColor;
    public AudioClip laserSound;
    AudioSource audioSource;

    override protected void Awake(){
        base.Awake();
        laserLine = GetComponent<LineRenderer>();
        laserLine.useWorldSpace = true;
        laserColor = laserLine.material.color;
        audioSource = GetComponent<AudioSource>();
    }
    override protected void Update(){
        base.Update();
        FadeLighting();
        if(!target){
            audioSource.Stop();//stop lasersound if not target
        }
    }
    override public void Activate()
    {   
        base.Activate();
        startPos = transform.Find("Turret/Laser Core").position;
        laserLine.SetPosition(0,startPos);
        InvokeRepeating("Fire", 0f, 1/attacksPerSecond); //finds new target and fires
    }
    // Update is called once per frame

    private void Fire(){
        if(!target && enemiesInRange.Count > 0){//try to get new target
            target = enemiesInRange[Random.Range(0,enemiesInRange.Count)];
        }
        if (target){
            DrawLightning();
            //target.GetComponent<Enemy>().health -= damage;
        }
    }

    private void DrawLightning(){
        if (audioSource != null && laserSound != null&& !audioSource.isPlaying){//play the laser sound if not already
            audioSource.PlayOneShot(laserSound);
        }
        laserLine.positionCount = 1; //resets Line positon count
        laserLine.material.color = laserColor; //sets base laser color
        float dist = Vector3.Distance(laserLine.GetPosition(0),target.position);//gets distance between lasercore and the target
        int interPoints = (int)Mathf.Floor(dist/5); //interpoint every 5 units of distance

        for(int i = 1; i <= interPoints; i++){//adds interpoints
            laserLine.positionCount++;
            Vector3 randDelta = Vector3.MoveTowards(laserLine.GetPosition(i-1),target.position,dist/(interPoints+1));//creates inter point with slightly randomized postion
            randDelta.x += Random.Range(-.5f,.5f); //randomizes x value
            randDelta.z += Random.Range(-.5f,.5f); //randomizes y value
            laserLine.SetPosition(i,randDelta);// sets the interpoint
        }//end for(int i = 1; i <= interPoints; i++)

        //sets last laser point at target
        laserLine.positionCount++;
        laserLine.SetPosition(interPoints+1,target.position);//creates point at target

    }//end DrawLightning()

    private void FadeLighting(){
        Color curColor = laserLine.material.color;//get current color of the Line

        if(curColor.a <= .05){//checks if Line is still visible, erases if not
            EraseLightning();
        }
        curColor.a -= Time.deltaTime;
        laserLine.material.color = curColor;
    }

    private void EraseLightning(){
        laserLine.positionCount = 1;
    }
}
