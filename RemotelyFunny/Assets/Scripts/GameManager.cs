using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int numSeconds = 10;
    [SerializeField] private int countDownSec = 5;
    [SerializeField] private float timerPenalty = .5f;
    private Score score;
    [SerializeField] private TextMeshProUGUI commandDisplay = default;
    [SerializeField] private TextMeshProUGUI countDownDisplay = default;
    [SerializeField] private Command[] tvCommands = default;
    //[SerializeField] private Command[] dvrCommands = default;
    //[SerializeField] private Command[] blenderCommands = default;
    [SerializeField] private bool debug = default;
    [SerializeField] private bool dvr = default;
    [SerializeField] private bool blender = default;

    private List<Command> commands;
    private Command currCommand;
    public Command CurrCommand => currCommand;

    private int numActionsCorrect = 0;


    /*
     * Called when the component is enabled. Helps with OnSceneLoaded()
     */
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*
     * Called when the component is disable, i.e. when the game ends. Helps with OnSceneLoaded()
     */
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /*
    * Decrease the timer. If time is 0 or less then go to game over screen
    */
    private void FixedUpdate()
    {
        // Only want the timer to go down if we're not in debug mode and we're currently in 
        // "Game" scene
        if (!debug && SceneManager.GetActiveScene().name.Equals("Game") && countDownDisplay.text.Equals(""))
        { 
            timeSlider.value -= Time.deltaTime / numSeconds;

            if (timeSlider.value <= 0)
            {
                score.SaveScore();
                countDownDisplay.text = countDownSec.ToString();
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    /*
     * Gets called everytime a scene is loaded. Checks if the current scene is the "Game" scene.
     * If so, then setups the scene
     */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene name: {scene.name}");
        if (scene.name.Equals("Game")) {
            SetUpScene();
        }
        else if (scene.name.Equals("GameOver"))
        {
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
            score.ShowRecentScore();
        }
    }

    public void CorrectAction()
    {
        numActionsCorrect++;
        score.ChangeScore((int)(timeSlider.value * 100)); // Change the score
        ResetTime(); // Got it right so reset time
        Random.InitState((int)System.DateTime.Now.Ticks); // Change seed so randomization is different
        currCommand = commands[Random.Range(0,commands.Count)]; // Choose a random index
        DisplayCommand(); // Display the next command
    }

    /*
     * Resets the time
     */
    public void ResetTime()
    {
        timeSlider.value = 1;
    }

    /*
     * Decreases the time based on the value passed in
     */
    public void DecreaseTime()
    {
        Debug.Log("Decreasing time");
        timeSlider.value -= timerPenalty / numSeconds;
    }

    /*
     * Sets up the scene by grabbing the necessary components and commands
     */
    private void SetUpScene()
    {
        // Grab components
        timeSlider = GameObject.FindGameObjectWithTag("Timer").GetComponent<Slider>();
        score = FindObjectOfType<Score>();
        commandDisplay = FindObjectOfType<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        countDownDisplay = GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>();

        // Make sure all components exist
        if (timeSlider == null || commandDisplay == null || countDownDisplay == null || score == null)
            Debug.LogError($"timeSlide: {timeSlider}, commandDisplay: {commandDisplay}, countDownDisplay: {countDownDisplay}, score: {score}");
        
        GrabCommands();

        if (commands.Count > 0) // Ensures that the commands list isn't empty
            currCommand = commands[0];
        else
            Debug.LogError("Commands array is empty");
        
        countDownDisplay.text = countDownSec.ToString();
        StartCoroutine(CountDown());
    }

    /*
     * Puts the different command lists into list and shuffles it so it's randomized
     */
    private void GrabCommands()
    {
        // TODO: Change which lists to grab from based on the round the player is on
        commands = new List<Command>(tvCommands);
        commands.Shuffle();
    } 

    /*
     * Counts down and displays the first command when finished
     */
    private IEnumerator CountDown()
    {
        while (countDownSec > 0)
        {
            countDownSec--;
            countDownDisplay.text = countDownSec.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        countDownDisplay.text = "";
        DisplayCommand();
    }

    /// <summary>
    /// Displays the command by changing the text
    /// </summary>
    public void DisplayCommand()
    {
        commandDisplay.text = currCommand.commandDisplay;
    }


}

/// <summary>
/// Helps shuffle lists. In our case, the list of commands
/// </summary>
public static class ShuffleHelper
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
