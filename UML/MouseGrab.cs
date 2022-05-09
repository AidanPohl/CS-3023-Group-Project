/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 24, 2022
*
* Description: Placing Towers
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGrab : MonoBehaviour
{   
    private GameManager gm;
    private Transform towerTrans;
    private int towerCost;  
    private BoundsCheck bndChk;
    private Tower towerScript;
    private GameObject haloGO;
    private bool canPlace;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM;
    }

    // Update is called once per frame
    void Update()
    {   //get mouse position from 2d coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = Camera.main.transform.position.y-.25f;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        transform.position = mousePos3D;
        if(towerTrans){
        towerTrans.position = mousePos3D;
        }
    }

    void LateUpdate(){
        if(transform.childCount >0){        if(towerTrans && towerScript.placeable && bndChk.isOnScreen){
            canPlace = true;
        }else{canPlace = false;}

        if(Input.GetMouseButtonDown(0) && canPlace){
            transform.DetachChildren();
            towerTrans.gameObject.GetComponent<Tower>().Activate();
            Clear();
        }else if (Input.GetMouseButtonDown(1)){
            Destroy(towerTrans.gameObject);
            gm.money += towerCost;
            Clear();
        }
        }
    }

    void Clear(){
        towerTrans = null;
        towerScript = null;
        bndChk = null;
        towerCost = 0;
    }
    public void NewObject(int tCost){
        towerTrans = transform.GetChild(0);
        towerScript = towerTrans.gameObject.GetComponent<Tower>();
        bndChk = towerTrans.gameObject.GetComponent<BoundsCheck>();
        towerCost = tCost;
    }

}
