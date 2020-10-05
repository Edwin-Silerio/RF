using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Performs any function on the player's score such as updating it, showing
/// it and saving them to playerprefsx.
/// </summary>
public class Score : MonoBehaviour
{
    // Private variables
    [SerializeField] TextMeshProUGUI scoreDisplay = default;
    private int score = 0;

    // Readonly variables
    private readonly string scoreKey = "RecentScore";
    private readonly string highscoresKey = "Highscores";
    private readonly int maxNumScores = 5;

    /*
     * Grab the score text
     */
    private void Awake()
    {
        //scoreDisplay = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Adds the int that's passed to the player's score and changes the display
    /// </summary>
    /// <param name="value">Value that's added to the player's score</param>
    public void ChangeScore(int value)
    {
        score = int.Parse(scoreDisplay.text) + value;
        scoreDisplay.text = score.ToString();
    }

    /// <summary>
    /// Saves the player's recent score so it can be displayed in the end 
    /// screen. Also checks if the score should be in the player's top five 
    /// scores.
    /// </summary>
    public void SaveScore()
    {
        // Save recent score
        PlayerPrefs.SetInt(scoreKey, int.Parse(scoreDisplay.text));
        int recentScore = PlayerPrefs.GetInt(scoreKey);

        List<int> highscores = new List<int>(PlayerPrefsX.GetIntArray(highscoresKey, 0, 5));
        
        // Check if recent score has beaten any previous scores
        for (int i = 0; i < highscores.Count; i++)
        {
            if (recentScore > highscores[i])
            {
                highscores.Insert(i, recentScore);
                break;
            }
        }

        // Remove any scores not in the top five
        for (int i = 5; i < highscores.Count;)
        {
            highscores.RemoveAt(maxNumScores);
        }
        
        // Save list of high scores
        PlayerPrefsX.SetIntArray(highscoresKey, highscores.ToArray());
    }

    /// <summary>
    /// Shows the player's most recent score.
    /// </summary>
    public void ShowRecentScore()
    {
        scoreDisplay.text = $"{PlayerPrefs.GetInt(scoreKey, 0)}";
    }

    /// <summary>
    /// Displays the player's top five scores.
    /// </summary>
    public void ShowHighscores()
    {
        scoreDisplay.text = "";
        List<int> highscores = new List<int>(PlayerPrefsX.GetIntArray(highscoresKey, 0, 5));
        for (int i = 0; i < maxNumScores; i++)
        {
            scoreDisplay.text += $"{i + 1}. {highscores[i]} {Environment.NewLine}";
        }
    }
}
