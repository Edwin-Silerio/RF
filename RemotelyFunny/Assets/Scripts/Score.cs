using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Changes the player's score
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private bool debug = false;

    private TextMeshProUGUI scoreDisplay = default;
    private KeyCode incScore = KeyCode.Period;
    private KeyCode decScore = KeyCode.Comma;
    private string scoreKey = "RecentScore";
    private string highscoresKey = "Highscores";
    private List<int> highscores;


    /*
     * Grab the score text
     */
    private void Awake()
    {

        scoreDisplay = GetComponent<TextMeshProUGUI>();
        //scoreDisplay.text = "0";
    }

    /*
     * Used for debugging. Press the . to increase the score. Press , to dec the score
     */
    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(incScore))
            {
                ChangeScore(10);
            }
            else if (Input.GetKeyDown(decScore))
            {
                ChangeScore(-10);
            }
        }
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
        highscores = new List<int>(PlayerPrefsX.GetIntArray(highscoresKey, 0, 5));
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
        scoreDisplay.text = $"{PlayerPrefs.GetInt(scoreKey, 0).ToString()}";
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
