using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Score score = default;

    void Start()
    {
        score.ShowRecentScore();
    }
}
