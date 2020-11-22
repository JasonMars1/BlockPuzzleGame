using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public GameState gameState; // scriptable object
    public Animator fadeAnimator;
    
    [SerializeField] float delayInSeconds = 4F;
    

    int currentSceneIndex;
    int splashTimeToWait = 3;

    
   



    private void Start()
    {
        fadeAnimator.enabled = false;

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            fadeAnimator.enabled = true;

            StartCoroutine(splashWaitForTime());
        }
    }

    IEnumerator splashWaitForTime()
    {
        yield return new WaitForSeconds(splashTimeToWait);
        
        // do fadeout animation with event trigger at end that calls FadeComplete()
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void FadeComplete()
    {
        fadeAnimator.enabled = false;
        LoadStartMenu();
    }


    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start");// load the start scene
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        gameState.ResetGame(); // go to other SOclass/script an destory text object

    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");

    }

    
    // grid manager no moves game over
    public void GameOver()
    {
        
        StartCoroutine(WaitAndLoad());
    }


    private IEnumerator WaitAndLoad()
    {
        // wait a few seconds then do next line
        yield return new WaitForSeconds(delayInSeconds);

        // after delay load gamerover
        SceneManager.LoadScene("Game Over");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
