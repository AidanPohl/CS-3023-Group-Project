/**
 * Created By: Aidan Pohl
 * Created: 02/23/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: 04/24/2022
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


[Header("Game Settings")]
public int lives = 50;
public int money = 0;
public int score = 0;

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

//High Score
[HideInInspector] public int highScore = 0;

    /***Methods***/
    void Awake(){
        CheckGameManagerIsInScene();
        //Checks for Highscore in Player Prefs
        if (PlayerPrefs.HasKey("HighScore")){
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        // Assign the values to Player Prefs
        PlayerPrefs.SetInt("HighScore",highScore);
    }//end Awake()

void Update(){ 
    //UnityEngine.Debug.Log(gameState);
    //check for new highscore and updates as neccessary


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
    GameObject.Find("Enemy Spawner").GetComponent<SpawnEnemy>().StartSpawning();

}//end StartGame();

public void GameScene(){
    SceneManager.LoadScene(gameMap); //load game Scene
}
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

public void SubLives(int damage){
    lives -= damage;
}
}
