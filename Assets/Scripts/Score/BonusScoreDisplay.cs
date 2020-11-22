using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScoreDisplay : MonoBehaviour
{
    public GameState gameState; // scriptable object
    Text bonusScoreText;

    [SerializeField] int bonusSetPoint;
    [SerializeField] int bonusActual;
    float rampRate = 50;

    [SerializeField] float rampUpScoreFloat;
    [SerializeField] int bonusRampScore;
    bool rampingUp;
    bool rampingDown;
        
    void Start()
    {
        bonusScoreText = GetComponent<Text>(); // script is on this object 
        bonusRampScore = gameState.GetBonusScore();
        rampingUp = false;
        rampingDown = false;
    }

    void Update()
    {

        bonusSetPoint = gameState.GetBonusScore();
         
        if(bonusSetPoint == 0)
        {
            bonusRampScore = 0;
        }

        if (bonusSetPoint > bonusRampScore)
        {
            rampingUp = true;
            rampUpScoreFloat += Time.deltaTime * rampRate;
            // convert float to int
            bonusRampScore = Mathf.RoundToInt(rampUpScoreFloat);
            
        }

        if (rampingUp == true && bonusSetPoint <= bonusRampScore)
        {
            bonusRampScore = bonusSetPoint;
            rampUpScoreFloat = bonusSetPoint;
            rampingUp = false;
        }

        if (bonusSetPoint < bonusRampScore)
        {
            rampingDown = true;
            rampUpScoreFloat -= Time.deltaTime * rampRate;
            // convert float to int
            bonusRampScore = Mathf.RoundToInt(rampUpScoreFloat);
        }

        if (rampingDown == true && bonusSetPoint >= bonusRampScore)
        {
            bonusRampScore = bonusSetPoint;
            rampUpScoreFloat = bonusSetPoint;
            rampingDown = false;
        }



        if (bonusSetPoint == bonusRampScore)
        {
            bonusActual = bonusSetPoint;
        }
        else
        {

            bonusActual = bonusRampScore;
        }

        
        // get score form gamesession and convert to string
        bonusScoreText.text = bonusActual.ToString();
        
    }
}
