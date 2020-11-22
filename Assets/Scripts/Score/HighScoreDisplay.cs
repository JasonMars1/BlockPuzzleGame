using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public GameState gameState; // scriptable object
    Text highScoreText;

    [SerializeField] int actualScore;
    [SerializeField] int actualRampingScore;
    [SerializeField] int highestScore;


    void Start()
    {
        highScoreText = GetComponent<Text>(); // script is on this Text object 


    }

    void Update()
    {
        // get current score
        actualScore = gameState.GetScore();
        // get ramping score
        actualRampingScore = gameState.GetRampScore();
        // get highest score
        highestScore = gameState.GetHighestScore();
        // get ramping score

        if (actualRampingScore == actualScore && actualScore > highestScore)
        {
            gameState.SubmitNewHighScore(actualScore);
        }

        if (actualRampingScore > highestScore)
        {
            highScoreText.text = actualRampingScore.ToString();
        }
        else
        {
            // get score form gamesession and convert to string
            highScoreText.text = highestScore.ToString();
        }
    }
}
