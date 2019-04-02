using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State game
public enum GameState
{
    menu, inGame, gameOver
}

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    // Call state
    public GameState currentGameState = GameState.menu;

    // Singleton
    public static GameManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Snake").GetComponent<PlayerController>();
    }

    /* 
    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
            Debug.Log("State Menu");
        }
        else if (newGameState == GameState.inGame)
        {
            MenuManager.sharedInstance.ShowInGame();
            Debug.Log("State InGame");

            // Iniciar JUEGO - Play Game
            playerController.StartMoveGame();
        }
        else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.ShowGameOver();
            Debug.Log("State Gameover");
        }
        this.currentGameState = newGameState;
    }
}
