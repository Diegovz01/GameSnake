using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public Canvas startCanvas;

    private void Awake() {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    // Enabled Menu
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        startCanvas.enabled = false;
    }

    // Enabled InGame
    public void ShowInGame()
    {
        menuCanvas.enabled = false;
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        startCanvas.enabled = false;
    }

    // Enabled GameOver
    public void ShowGameOver()
    {
        menuCanvas.enabled = false;
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        startCanvas.enabled = false;
    }

    // Enabled Start
    public void ShowStart()
    {
        menuCanvas.enabled = false;
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        startCanvas.enabled = true;
    }
    // Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }


}
