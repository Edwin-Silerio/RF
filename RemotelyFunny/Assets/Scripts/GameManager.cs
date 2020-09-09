using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int secondsPerCommand = 10;
    [SerializeField] private int countDownSeconds = 5;
    [SerializeField] private float timerPenalty = .5f;
    [SerializeField] private TextMeshProUGUI countDownDisplay = default;
    [SerializeField] private GameObject speechBubble = default;

    [Header("Commands")]
    [SerializeField] private Command[] tvCommands = default;
    [SerializeField] private Command[] dvrCommands = default;
    //[SerializeField] private Command[] blenderCommands = default;

    [Header("Remotes and Devices")]
    [SerializeField] private bool addDvrRemote = default;
    [SerializeField] private GameObject dvrBox = default;
    [SerializeField] private GameObject tableDVRRemote = default;
    [SerializeField] private bool addBlenderRemote = default;
    [SerializeField] private GameObject blender = default;
    [SerializeField] private GameObject tableBlenderRemote = default;
    [SerializeField] private bool debug = default;

    private TextMeshProUGUI commandDisplay;
    private Score score;
    private List<Command> commands;
    private Command currCommand;
    public Command CurrCommand => currCommand;
    public bool GetDVR => addDvrRemote;
    public bool GetBlender => addBlenderRemote;

    private int numActionsCorrect = 0;
    private int currRound = -1;


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
            timeSlider.value -= Time.deltaTime / secondsPerCommand;

            if (timeSlider.value <= 0)
            {
                score.SaveScore();
                countDownDisplay.text = countDownSeconds.ToString();
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
        //Debug.Log($"Scene name: {scene.name}");
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
        if (numActionsCorrect < 10)
        {
            score.ChangeScore((int)(timeSlider.value * 100)); // Change the score
            ResetTime(); // Got it right so reset time

            int n = Random.Range(1, commands.Count);// Choose a random index
            currCommand = commands[n]; 
            // Moves the selected command to index 0 so it's not chosen again right away
            commands[n] = commands[0];
            commands[0] = currCommand;
            DisplayCommand(); // Display the next command
        }
        else
            NextRound();
        
    }

    public void NextRound()
    {
        currRound++;
        if(currRound == 0)
        {
            dvrBox.SetActive(false);
            tableDVRRemote.SetActive(false);
        }
        else if (currRound == 1)
        {
            Debug.Log("Round 2");
            dvrBox.gameObject.SetActive(true);
            tableDVRRemote.SetActive(true);
            addDvrRemote = true;
        }
        else if(currRound == 2)
        {
            Debug.Log("Made it to round 3!");
            currRound++;
            addBlenderRemote = true;

        }

        GrabCommands();
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
        timeSlider.value -= timerPenalty / secondsPerCommand;
    }

    /*
     * Sets up the scene by grabbing the necessary components and commands
     */
    private void SetUpScene()
    {
        // Grab components
        /* timeSlider = GameObject.FindGameObjectWithTag("Timer").GetComponent<Slider>();

         countDownDisplay = GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>();
 */
        score = FindObjectOfType<Score>();
        commandDisplay = speechBubble.GetComponentInChildren<TextMeshProUGUI>();
        tableDVRRemote = GameObject.FindGameObjectWithTag("TableDVR");
        dvrBox = GameObject.FindGameObjectWithTag("DVR");
        timeSlider.value = 1;
        score.ChangeScore(0);
        currRound = -1;


        Random.InitState((int)System.DateTime.Now.Ticks); // Change seed so randomization is different

        // Make sure all components exist
        if (timeSlider == null || commandDisplay == null || countDownDisplay == null || score == null)
            Debug.LogError($"timeSlide: {timeSlider}, commandDisplay: {commandDisplay}, countDownDisplay: {countDownDisplay}, score: {score}");

        NextRound();

        if (commands.Count > 0) // Ensures that the commands list isn't empty
            currCommand = commands[0];
        else
            Debug.LogError("Commands array is empty");
        
        countDownDisplay.text = countDownSeconds.ToString();
        StartCoroutine(CountDown());
    }

    /*
     * Puts the different command lists into list and shuffles it so it's randomized
     */
    private void GrabCommands()
    {
        // TODO: Change which lists to grab from based on the round the player is on
        commands = new List<Command>(tvCommands);
        numActionsCorrect = 0;
        if (addDvrRemote)
        {
            Debug.Log("Grabbing the dvr commands");
            foreach(Command command in dvrCommands)
                commands.Add(command);
        }
        commands.Shuffle();
    } 

    /*
     * Counts down and displays the first command when finished
     */
    private IEnumerator CountDown()
    {
        while (countDownSeconds > 0)
        {
            countDownSeconds--;
            countDownDisplay.text = countDownSeconds.ToString();
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
        speechBubble.gameObject.SetActive(true);
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
