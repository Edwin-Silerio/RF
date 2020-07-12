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


    /*
     * Grab the score text
     */
    private void Awake()
    {
        Debug.Log("Grabbing textmesh");
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        Debug.Log($"Textmesh: {scoreDisplay.transform.parent.gameObject.name}");
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
    }

    public void ShowRecentScore()
    {
        Debug.Log("In showrecentscore");
        scoreDisplay.text = $"Score: {PlayerPrefs.GetInt(scoreKey, 0).ToString()}";
    }
}
