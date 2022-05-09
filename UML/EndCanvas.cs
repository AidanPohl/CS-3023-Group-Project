/**
 * Created By: Aidan Pohl
 * Created: 03/06/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: Apr 24, 2022
 * 
 * Description: Update global text in end screen canvas
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Reference to user interface

public class EndCanvas : MonoBehaviour
{       /***VARIABLES***/
    GameManager gm; //reference to the game manager

    [Header("Canvas Settings")]
    public Text timer;
    public Text score;
    public int totalScore;
    public Text highscore;



    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM;
        TimeSpan survivedTime = GameManager.timer.Elapsed.Duration();
        totalScore = gm.score + (int)Math.Floor(survivedTime.TotalSeconds);
        timer.text ="You lasted "+ survivedTime.ToString(@"hh\:mm\:ss");
        score.text ="Final score: "+totalScore;
        //check to see if beat previous high score
        if(totalScore.CompareTo(gm.highScore) > 0){ //better than highscore
            highscore.text = "New High Score";
            //Update highscore in Player Prefs
            PlayerPrefs.SetInt("Highscore",totalScore);
        }else{//Slower than BestTime
            highscore.text = "HighScore: " + gm.highScore;
        }//end if else
    }//end Start()

    public void StartScreen(){
     gm.StartScreen();
    }//end StartScreen()

    public void ExitGame(){
     gm.ExitGame();
    }//end ExitGame()
}
