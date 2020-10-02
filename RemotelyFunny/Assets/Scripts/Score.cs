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
    private TextMeshProUGUI scoreDisplay = default;
    private List<int> highscores;
    private int score = 0;

    // Readonly variables
    private readonly string scoreKey = "RecentScore";
    private readonly string highscoresKey = "Highscores";


    /*
     * Grab the score text
     */
    private void Awake()
    {

        scoreDisplay = GetComponent<TextMeshProUGUI>();
        //scoreDisplay.text = "0";
    }

    /// <summary>
    /// Adds the int that's passed to the player's score and changes the display
    /// </summary>
    /// <param name="value"></param>
    public void ChangeScore(int value)
    {
        score = int.Parse(scoreDisplay.text) + value;
        scoreDisplay.text = score.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(scoreKey, int.Parse(scoreDisplay.text));
        int recentScore = PlayerPrefs.GetInt(scoreKey);
        List<int> highscores = new List<int>(PlayerPrefsX.GetIntArray(highscoresKey, 0, 5));
        Debug.Log($"highscores count: {highscores.Count}");
        for (int i = 0; i < highscores.Count; i++)
        {
            if (recentScore > highscores[i])
            {
                highscores.Insert(i, recentScore);
                PlayerPrefsX.SetIntArray(highscoresKey, highscores.ToArray());
                break;
            }
        }

        for (int i = 5; i < highscores.Count; i++)
        {
            highscores.RemoveAt(i);
        }

        foreach (int score in highscores)
        {
            Debug.Log(score);
        }
    }

    public void ShowRecentScore()
    {
        Debug.Log("In showrecentscore");
        scoreDisplay.text = $"{PlayerPrefs.GetInt(scoreKey, 0)}";
    }

    public void ShowHighscores()
    {
        scoreDisplay.text = "";
        highscores = new List<int>(PlayerPrefsX.GetIntArray(highscoresKey, 0, 5));
        for (int i = 0; i < 5; i++)
        {
            scoreDisplay.text += $"{i + 1}. {highscores[i]} { Environment.NewLine}";
        }
    }
}
