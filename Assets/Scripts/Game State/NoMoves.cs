using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoMoves : MonoBehaviour
{
    public GameState gameState; // scriptable object
    public GameObject NoMovesPopup; // link in inspector
    public GameObject GOBonusPtsStop; //stop image
    public GameObject GOBonusBtnGlow; // glow image
    [SerializeField] Text countDownText;
    [SerializeField] float gameOverDelayInSeconds = 4F;
    [SerializeField] float delayInSeconds = 10F;
    [SerializeField] float countDownInSeconds = 0F;
    [SerializeField] bool popupActive = false;
    [SerializeField] int gameOverAdCount = 0;
    [SerializeField] int popupOpenCount = 0;
    [SerializeField] AudioClip gameoverSound;

    bool pulseOn = false;
    float pulseTime = 0.0f;

    public void Start()
    {
        popupOpenCount = 0;
        gameState.GameOverAdvertReset();
        countDownInSeconds = gameOverDelayInSeconds;
    }

    public void Update()
    {
        gameOverAdCount = gameState.GetGameOverAdvert();

        // gameover ad button pressed close the popup
        if(gameOverAdCount == 1 && popupOpenCount == 1)
        {
             CloseNoMovesPopUp();
            
        }
        

        if (popupActive == true)
        {
            countDownInSeconds = countDownInSeconds - Time.deltaTime;
            countDownText.text = countDownInSeconds.ToString("0");

            if (countDownInSeconds <= 0)
            {
                countDownInSeconds = 0;
                gameState.GameOverAdvertReset();
                popupActive = false;
                popupOpenCount = 0;
                // after delay load gameover
                FindObjectOfType<Level>().LoadGameOver();
            }
        }

        // advert ready == true, not ready == false
        if(gameState.GetAdvertStatus() == false || gameOverAdCount == 1)
        {
            // turn on blocking image
            GOBonusPtsStop.SetActive(true);
        }
        else
        {
            // turn off blocking image
            GOBonusPtsStop.SetActive(false);
        }

        // countdown text visability
        if(popupOpenCount == 2)
        {
            countDownText.gameObject.SetActive(false);
        }
        else
        {
            countDownText.gameObject.SetActive(true);
        }

        // button glow control
        pulseTime = pulseTime + Time.deltaTime;

        if (pulseTime >= 1)
        {
            pulseTime = 0.0f;
        }

        if (pulseTime >= 0.0f && pulseTime <= 0.5f)
        {
            pulseOn = true;
        }
        else
        {
            pulseOn = false;
        }


        if (popupOpenCount == 1)
        {
            if (pulseOn)
            {
                GOBonusBtnGlow.SetActive(true);
            }
            else
            {
                GOBonusBtnGlow.SetActive(false);
            }
        }
        else
        {
            GOBonusBtnGlow.SetActive(false);
        }

    }

    // call from grid manager when no moves available
    public void OpenNoMovesPopUp()
    {
        // display popup image
        NoMovesPopup.SetActive(true);
        popupActive = true;
        AudioSource.PlayClipAtPoint(gameoverSound, Camera.main.transform.position, 0.75f);

        //longer delay for first display of gameover popup
        if (popupOpenCount == 0)
        { countDownInSeconds = delayInSeconds; }
        else { countDownInSeconds = gameOverDelayInSeconds; }
        
        popupOpenCount = popupOpenCount + 1;
              
    }

    
    public void CloseNoMovesPopUp()
    {
        // close popup image
        NoMovesPopup.SetActive(false);
        popupActive = false;
    }
 }

