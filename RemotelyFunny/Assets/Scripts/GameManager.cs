using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider timeSlider = default;
    [SerializeField] private int numSeconds = 1;


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


}
