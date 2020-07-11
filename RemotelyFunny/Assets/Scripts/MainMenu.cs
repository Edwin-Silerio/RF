using System.Collections;
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

    /// <summary>
    /// Goes to the game scene
    /// </summary>
    public void GoToGame()
    {
        Debug.Log("Playing game");
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Displays the highscores
    /// </summary>
    public void ShowScores()
    {
        Debug.Log("Showing scores");
        menu.alpha = 0;
        menu.blocksRaycasts = false;
        menu.interactable = false;
        highscores.alpha = 1;
        highscores.blocksRaycasts = true;
        highscores.interactable = true;
    }

    /// <summary>
    /// Hides the scores
    /// </summary>
    public void HideScores()
    {
        Debug.Log("Showing menu");
        highscores.alpha = 0;
        highscores.blocksRaycasts = false;
        highscores.interactable = false;
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
