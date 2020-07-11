using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int numSeconds = 1;
    [SerializeField] private TextMeshProUGUI commandDisplay = default;
    [SerializeField] private Command[] tvCommands = default;
    //[SerializeField] private Command[] dvrCommands = default;
    //[SerializeField] private Command[] blenderCommands = default;
    [SerializeField] private bool debug = default;

    private List<Command> commands;
    private Command currCommand;
    private static GameManager instance;
    public static GameManager Instance => instance;

    private int index = 0;

    /*
     * Allows for the game manager to be a singleton
     */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

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
     * Primarily used for debugging. P cycles through the command and R reloads the scene
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DisplayCommand();
            index++;
            if (index < commands.Count)
                currCommand = commands[index];
            else
                Debug.Log("Out of commands");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }

    /*
    * Decrease the timer. If time is 0 or less then go to game over screen
    */
    private void FixedUpdate()
    {
        // Only want the timer to go down if we're not in debug mode and we're currently in 
        // "Game" scene
        if (!debug && SceneManager.GetActiveScene().name.Equals("Game"))
        { 
            timeSlider.value -= Time.deltaTime / numSeconds;

            if (timeSlider.value <= 0)
            {
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
        if (scene.name.Equals("Game")) {
            SetUpScene();
        }
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
    public void DecreaseTime(float time)
    {
        Debug.Log("Decreasing time");
        timeSlider.value -= time/numSeconds;
    }

    /*
     * Sets up the scene by grabbing the necessary components and commands
     */
    private void SetUpScene()
    {
        timeSlider = FindObjectOfType<Slider>();
        commandDisplay = FindObjectOfType<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        GrabCommands();

        if (commands.Count > 0) // Ensures that the commands list isn't empty
            currCommand = commands[0];
        else
            Debug.LogError("Commands array is empty");
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
