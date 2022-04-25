/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 25, 2022
*
* Description: Shop Interaction
**/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class ShopCanvas : MonoBehaviour
{   
    public GameManager gm;
    [Header("Set In Inspector")]
    public Text livesText;
    public Text moneyText;
    public Text timerText;
    public Stopwatch timer;
    [Space(10)]
    [Header("Shop")]
    public GameObject[] buttons;
    public GameObject[] towerPrefabs;
    public int[] towerPrices;

    public Transform mouseAttach;

    void Awake(){

    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM;
        timer = GameManager.timer;
    }

    // Update is called once per frame
    void Update()
    {   TimeSpan curTime = GameManager.timer.Elapsed.Duration();
        //update text boxes
        livesText.text = "Lives: "+ gm.lives;
        moneyText.text = "Money: "+ gm.money;
        timerText.text = "Timer: "+ curTime.ToString(@"hh\:mm\:ss");
//        levelText.text = "Round: "+GameManager.level;

        //update buttons
        for(int i=0; i<buttons.Length;i++){
            if(towerPrices[i]<=gm.money && mouseAttach.childCount==0) {//if affordable and not currently placing a tower
                buttons[i].GetComponent<Button>().interactable = true;
            } else{
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void PurchaseTower(int item){
        Instantiate(towerPrefabs[item],mouseAttach);
        gm.money -= towerPrices[item];
        mouseAttach.gameObject.GetComponent<MouseGrab>().NewObject(towerPrices[item]);
    }


    


}
