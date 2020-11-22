using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public GameState gameState; // scriptable object
    Text scoreText;

    [SerializeField] int scoreSetPoint;
    [SerializeField] int scoreActual;
    float rampRate = 50;

    [SerializeField] float rampUpScoreFloat;
    [SerializeField] int rampScore;


    void Start()
    {
        scoreText = GetComponent<Text>(); // script is on this object 
        rampScore = gameState.GetScore();
    }

    void Update()
    {

        scoreSetPoint = gameState.GetScore();


        if(scoreSetPoint == 0)
        {
            rampScore = 0;
        }

        //score ramping
        if (scoreSetPoint > rampScore)
        {
            rampUpScoreFloat += Time.deltaTime * rampRate;
            // convert float to int
            rampScore = Mathf.RoundToInt(rampUpScoreFloat);           
        }

        

        // score display
        if (scoreSetPoint <= rampScore)
        {
            scoreActual = scoreSetPoint;
            rampScore = scoreSetPoint;
            rampUpScoreFloat = scoreSetPoint;
        }
        else
        {

            scoreActual = rampScore;
        }

        //for high score ramping
        gameState.RampingScore(rampScore);
        

        // get score form gamesession and convert to string
        scoreText.text = scoreActual.ToString();


    }
}
