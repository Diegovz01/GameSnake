using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;

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
    }

    // Enabled InGame
    public void ShowInGame()
    {
        menuCanvas.enabled = false;
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }

    // Enabled GameOver
    public void ShowGameOver()
    {
        menuCanvas.enabled = false;
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
    }

    // Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }


}
