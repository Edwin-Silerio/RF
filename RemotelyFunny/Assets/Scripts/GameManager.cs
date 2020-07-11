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
     * Set up the scene in the beginning
     */
    private void Start()
    {
        SetUpScene();
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
        if (!debug && SceneManager.GetActiveScene().name.Equals("Game"))
        { 
            timeSlider.value -= Time.deltaTime / numSeconds;

            if (timeSlider.value <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    private void SetUpScene()
    {
        GrabCommands();

        if (commands.Count > 0) // Ensures that the commands list isn't empty
            currCommand = commands[0];
        else
            Debug.LogError("Commands array is empty");
    }

    private void GrabCommands()
    {
        commands = new List<Command>(tvCommands);
        commands.Shuffle();
    } 

   

    public void DisplayCommand()
    {
        commandDisplay.text = currCommand.commandDisplay;
    }


}

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
