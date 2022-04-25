/**
* Created By: Aidan Pohl
* Created: Apr 24, 2022
*
*
* Last Edited By:
* Last Edited: Apr 24, 2022
*
* Description: Shop Interaction
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{   
    GameManager gm;
    [Header("Set In Inspector")]
    public Text livesText;
    public Text moneyText;
    public Text levelText;

    [Space(10)]
    [Header("Shop")]
    public GameObject[] buttons;
    public GameObject[] towerPrefabs;
    public int[] towerPrices;

    public Transform mouseAttach;

    void Awake(){
        gm = GameManager.GM;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //update text boxes
        livesText.text = "Lives: "+gm.lives;
        moneyText.text = "Money: "+gm.money;
        levelText.text = "Round: "+gm.level;

        //update buttons
        for(int i=0; i<buttons.Length;i++){
            if(towerPrices[i]<gm.money && mouseAttach.childCount==0) {//if affordable and not currently placing a tower
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
