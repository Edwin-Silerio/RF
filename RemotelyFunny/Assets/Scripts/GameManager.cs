using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Unity.IO;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int numSeconds = 1;
    [SerializeField] private TextMeshProUGUI commandDisplay = default;
    [SerializeField] private Command command = default;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DisplayCommand();
        }
    }

    /*
     * Decrease the timer. If time is 0 or less then go to game over screen
     */
    private void FixedUpdate()
    {
        timeSlider.value -= Time.deltaTime/numSeconds;

        if(timeSlider.value <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void DisplayCommand()
    {
        commandDisplay.text = command.commandDisplay;
    }


}
