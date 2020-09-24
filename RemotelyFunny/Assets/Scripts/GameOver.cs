using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Score score = default;

    // Start is called before the first frame update
    void Start()
    {
        score.ShowRecentScore();
    }
}
