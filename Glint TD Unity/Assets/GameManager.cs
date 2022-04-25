/**
 * Created By: Aidan Pohl
 * Created: 02/23/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: 04/23/2022
 * 
 * Description: Game Managaer
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    /***VARIABLES***/
    #region GameManager Singleton
    static private GameManager gm; // reference to Game Manager
    static public GameManager GM  { get {   return gm;  }   }//public access to Game manager

    public void CheckGameManagerIsInScene(){
        if (gm == null){
            gm = this;
        }else{
            Destroy(this.gameObject);
        }//end if else
        DontDestroyOnLoad(this); //Do not destroy the game manager when new scene is loaded
    }//end CheckGameManagerIsInScene()
    
    #endregion

[Header("General Settings")]
public string gameTitle = "Glint TD";
public string gameCredits = "Made by: Aidan Pohl, ";
public string copywriteDate = "Copyright " + thisDate;
public static int score = 0;
public static int lives = 50;

[Header("Game Settings")]
[Header("Scene Settings")]
[Tooltip("Name of start scene")]
public string startString;

[Tooltip("Name of end Scene")]
public string endScene;

[Tooltip("Name of Map scene")]
public string gameMap;



[HideInInspector] public enum gameStates {Idle,Playing,StartScreen,GameLose};//enum of game states
[HideInInspector] public static gameStates gameState = gameStates.StartScreen; //curent gamestate
[HideInInspector] public static Stopwatch timer = new Stopwatch();
private static string thisDate = System.DateTime.Now.ToString("yyyy"); //todays date as string

//Best time in three parts
[HideInInspector] public static int bestHours = 0;
[HideInInspector] public static int bestMins = 0;
[HideInInspector] public static int bestSecs = 0;
[HideInInspector] public static TimeSpan BestTime;
[HideInInspector] public static int highScore = 0;

    /***Methods***/
    void Awake(){
    CheckGameManagerIsInScene();

        //Checks for BestTime in Player pref
        if (PlayerPrefs.HasKey("BestHours"))
        {
            bestHours = PlayerPrefs.GetInt("BestHours");
            bestMins = PlayerPrefs.GetInt("BestMins");
            bestSecs = PlayerPrefs.GetInt("BestSecs");
        } 
        //Checks for Highscore in Player Prefs
        if (PlayerPrefs.HasKey("HighScore")){
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        // Assign the values to Player Prefs
        PlayerPrefs.SetInt("HighScore",highScore);
<<<<<<< Updated upstream
        PlayerPrefs.SetInt("BestHours", bestHours);
        PlayerPrefs.SetInt("BestMins", bestMins);
        PlayerPrefs.SetInt("BestSecs", bestSecs);
        BestTime =  new TimeSpan(bestHours, bestMins, bestSecs);
=======


>>>>>>> Stashed changes
    }//end Awake()

void Update(){ 
    UnityEngine.Debug.Log(gameState);
    //check for new highscore and updates as neccessary
    if(score>highScore){
        highScore=score;
        PlayerPrefs.SetInt("HighScore",highScore);
    }

    if(Input.GetKey("escape")){ExitGame();} //esc key to exit game


    if(lives<=0 && gameState == gameStates.Playing){ //if all lives agone
        lives = 0;
        timer.Stop();
        gameState = gameStates.Idle;
        Invoke("GameEnd",5f);
    }
}//end Update

public void StartGame(){
    gameState = gameStates.Playing;//playing game state
    timer = Stopwatch.StartNew();
    SceneManager.LoadScene(gameMap); //load first level

}//end StartGame();

public void ExitGame(){
    Application.Quit();
    UnityEngine.Debug.Log("Exited Game");
}//end ExitGame

public void GameEnd(){
    gameState = gameStates.GameLose;//game end state
    SceneManager.LoadScene(endScene);
    UnityEngine.Debug.Log("Game End Scene");
}//end GameEnd()


public void StartScreen(){
    SceneManager.LoadScene(startString);
    gameState=gameStates.StartScreen;
}//end StartScreen()

}
