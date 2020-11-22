using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPopup; // link in inspector
    
    public Toggle soundToggle; // link in inspector
    public GameState gameState; // scriptable object
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    // function connected to toggle button on value change
    public void AudioToggle(bool audioToggle)
    {
        if (audioToggle == true)
        {
            gameState.SetAudioMode(1);
        }
        else
        {
            gameState.SetAudioMode(0);
        }
    }

    public void OpenSettingsPopup()
    {
        // display settings popup image with buttons
        SettingsPopup.SetActive(true);

        // get audio mode from game state player prefs
        if (gameState.GetAudioMode() == 1)
        {
            soundToggle.isOn = true;
        }
        else
        {
            soundToggle.isOn = false;
        }
    }

    

    public void CloseSettingsPopup()
    {
        // close settings popup image with buttons
        SettingsPopup.SetActive(false);
    }


}
