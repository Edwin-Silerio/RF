using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Unity 
    [Header("HUD")]
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int secondsPerCommand = 10;
    [SerializeField] private int countDownSeconds = 5;
    [SerializeField] private float timerPenalty = .5f;
    [SerializeField] private TextMeshProUGUI countDownDisplay = default;
    [SerializeField] private GameObject speechBubble = default;
    [SerializeField] private TextMeshProUGUI commandDisplay = default;
    [SerializeField] private Score score = default;

    [Header("Commands")]
    [SerializeField] private Command[] tvCommands = default;
    [SerializeField] private Command[] dvrCommands = default;
    [SerializeField] private Command[] blenderCommands = default;

    [Header("Remotes and Devices")]
    [SerializeField] private bool addDvrRemote = default;
    [SerializeField] private GameObject dvrBox = default;
    [SerializeField] private GameObject tableDVRRemote = default;
    [SerializeField] private bool addBlenderRemote = default;
    [SerializeField] private GameObject blender = default;
    [SerializeField] private GameObject tableBlenderRemote = default;

    [Header("")]
    [SerializeField] private bool debug = default;

    #endregion

    #region Private
    private List<Command> commands;
    private bool hasRoundStarted = false;
    private int numActionsCorrect = 0;
    private int currRound = -1;
    #endregion

    #region Getters Setters
    public Command CurrCommand { get; private set; }
    public bool GetDVR => addDvrRemote;
    public bool GetBlender => addBlenderRemote;
    #endregion

    private void Start()
    {
        SetUpScene();
    }

    /*
    * Decrease the timer. If time is 0 or less then go to game over screen.
    */
    private void FixedUpdate()
    {
        // Only want the timer to go down if we're not in debug mode and the 
        // round has started.
        if (!debug && hasRoundStarted)
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
     * Sets up the scene by grabbing the necessary components and commands
     */
    private void SetUpScene()
    {
        timeSlider.value = 1;
        score.ChangeScore(0);
        currRound = -1;


        Random.InitState((int)System.DateTime.Now.Ticks); // Change seed so randomization is different

        // Make sure all components exist
        if (timeSlider == null || commandDisplay == null || countDownDisplay == null || score == null)
            Debug.LogError($"timeSlide: {timeSlider}, commandDisplay: {commandDisplay}, countDownDisplay: {countDownDisplay}, score: {score}");

        NextRound();

        if (commands.Count > 0) // Ensures that the commands list isn't empty
            CurrCommand = commands[0];
        else
            Debug.LogError("Commands array is empty");
        
        countDownDisplay.text = countDownSeconds.ToString();
        StartCoroutine(CountDown());
    }
    
    /* 
     * Increments the current round number and activates objects based on the
     * current round. Then grabs the correct commands. Round 1 - Tv. 
     * Round 2 - Tv, and Dvr. Round 3 - Tv, Dvr, and Blender.
     */
    private void NextRound()
    {
        currRound++;
        // Round 1
        if (currRound == 0)
        {
            dvrBox.SetActive(false);
            tableDVRRemote.SetActive(false);
            blender.SetActive(false);
            tableBlenderRemote.SetActive(false);

        }
        // Round 2
        else if (currRound == 1)
        {
            Debug.Log("Made it to round 2!");
            dvrBox.gameObject.SetActive(true);
            tableDVRRemote.SetActive(true);
            addDvrRemote = true;
        }
        // Round 3
        else if (currRound == 2)
        {
            Debug.Log("Made it to round 3!");
            blender.SetActive(true);
            tableBlenderRemote.SetActive(true);
            addBlenderRemote = true;

        }
        GrabCommands();
    }

    /*
     * Puts the different command lists into list and shuffles it so it's randomized
     */
    private void GrabCommands()
    {
        // TODO: Change which lists to grab from based on the round the player is on
        commands = new List<Command>(tvCommands);
        numActionsCorrect = 0;
        if(addDvrRemote)
        {
            Debug.Log("Grabbing the dvr commands");
            foreach(Command command in dvrCommands)
            {
                commands.Add(command);
            }
        }
        if (addBlenderRemote)
        {
            Debug.Log("Grabbing the blender commands");
            foreach(Command command in blenderCommands)
            {
                commands.Add(command);
            }
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
        hasRoundStarted = true;
        DisplayCommand();
    }

    /// <summary>
    /// Increments the total number of correct actions and changes the score. 
    /// If the player has 10 or more correct actions, move on to the next round.
    /// </summary>
    public void CorrectAction()
    {
        numActionsCorrect++;
        if (numActionsCorrect < 10)
        {
            score.ChangeScore((int)(timeSlider.value * 100)); // Change the score
            ResetTime(); // Got it right so reset time

            int n = Random.Range(1, commands.Count);// Choose a random index
            CurrCommand = commands[n]; 
            // Moves the selected command to index 0 so it's not chosen again right away
            commands[n] = commands[0];
            commands[0] = CurrCommand;
            DisplayCommand(); // Display the next command
        }
        else
            NextRound();
        
    }

    /// <summary>
    /// Resets the time bar to the starting time.
    /// </summary>
    public void ResetTime()
    {
        timeSlider.value = 1;
    }

    /// <summary>
    /// Decreases the time based on the timer penalty.
    /// </summary>
    public void DecreaseTime()
    {
        Debug.Log("Decreasing time");
        timeSlider.value -= timerPenalty / secondsPerCommand;
        
    }


    /// <summary>
    /// Displays the current command on the speech bubble.
    /// </summary>
    public void DisplayCommand()
    {
        speechBubble.gameObject.SetActive(true);
        commandDisplay.text = CurrCommand.commandDisplay;
    }


}

/// <summary>
/// Helps shuffle lists. In our case, the list of commands
/// </summary>
public static class ShuffleHelper
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
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
