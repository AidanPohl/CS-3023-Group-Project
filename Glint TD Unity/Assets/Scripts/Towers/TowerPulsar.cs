/**
 * Created By: Aidan Pohl
 * Date Created: April 20, 2022
 * 
 * Last Edited By:
 * Date Last Edited: April 21, 2022
 * 
 8 Description: Funny object do the pulse
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class TowerPulsar : Tower
{
    /**VARIABLES**/

    [Header("Attack Stats")]
    public float attacksPerSecond = 1/3; //How quickly it attacks
    public int attackStrength = 3; //How much damage each attack does
    private GameObject pulsar;
    private SphereCollider pulse;
    private bool pulsing;

    private Color coreColor;
    private Color coreColorTransparent;
    private float radius;
    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        pulsar = transform.Find("Tower pulsar/Pulsar").gameObject;
        pulse = pulsar.GetComponent<SphereCollider>();
        radius = pulse.radius;
        coreColor = pulsar.GetComponent<Renderer>().material.color;
        coreColorTransparent = coreColor;
        coreColorTransparent.a =0;
        pulsar.GetComponent<Attack_Pulse>().damage = attackStrength;


    }

    override public void Activate()
    {
        base.Activate();
        InvokeRepeating("Pulse", 0f, 1/attacksPerSecond); //fires
    }

    // Update is called once per frame
    override protected void Update()
    {   
        base.Update();
        Quaternion tureetRot = transTurret.rotation;
        Vector3 rot = tureetRot.eulerAngles;
        rot.y += .1f;
        tureetRot.eulerAngles = rot;
        transTurret.rotation = tureetRot;

        if(pulsing){
            radius = Mathf.Lerp(radius,range,5*Time.deltaTime/attacksPerSecond);
            pulsar.transform.localScale = Vector3.one*radius*2;
            pulsar.GetComponent<Renderer>().material.color = Color.Lerp(coreColor,coreColorTransparent,radius/range);
        }
    }



<<<<<<< Updated upstream
    private void Pulse()
    {   
=======
    private void Pulse(){

>>>>>>> Stashed changes
        radius = .1f;
        pulsar.transform.localScale = Vector3.one*.1f;
        Debug.Log(enemiesInRange.Count + "enemies");
        if (enemiesInRange.Count > 0) {
            pulsing = true;
            if(audioSource!=null && pulseSound!=null){audioSource.PlayOneShot(pulseSound);}//plays pulse sound
        }else{
            pulsing = false;}
    }//end Pulse();

}
