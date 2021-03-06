﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the menu buttons
/// </summary>
public class MainMenu: MonoBehaviour
{
    [SerializeField] private float quitDelay = 1f;
    [SerializeField] private CanvasGroup highscores = default;
    [SerializeField] private CanvasGroup menu = default;
    [SerializeField] private Score score = default;

    /// <summary>
    /// Goes to the game scene
    /// </summary>
    public void GoToGame()
    {
        Debug.Log("Playing game");
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Goes to the main menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Displays the highscores
    /// </summary>
    public void ShowScores()
    {
        // Hide the menu
        menu.alpha = 0;
        menu.blocksRaycasts = false;
        menu.interactable = false;

        // Show scores
        highscores.alpha = 1;
        highscores.blocksRaycasts = true;
        highscores.interactable = true;
        score.ShowHighscores();

    }

    /// <summary>
    /// Hides the scores
    /// </summary>
    public void HideScores()
    {
        Debug.Log("Showing menu");
        // Hide the scores
        highscores.alpha = 0;
        highscores.blocksRaycasts = false;
        highscores.interactable = false;

        // Show the menu
        menu.alpha = 1;
        menu.blocksRaycasts = true;
        menu.interactable = true;
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit the game");
        StartCoroutine(QuitAfterDelay());
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(quitDelay);
        Application.Quit();
    }

}
