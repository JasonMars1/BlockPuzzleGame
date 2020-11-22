using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// for scriptable object create state object in the assests menu
[CreateAssetMenu(menuName = "GameStateSO")]

public class GameState : ScriptableObject
{
    [SerializeField] int score = 0;
    [SerializeField] int rampingScore = 0;
    [SerializeField] int bonusScore = 0;
    [SerializeField] int bonusLevel = 100;
    [SerializeField] int bonusAmount = 10;
    [SerializeField] int bonusLevelTarget = 0;
    [SerializeField] int bonusHoldAmount = 10;
    [SerializeField] int bonusNewAmount = 20;
    [SerializeField] int bonusDeleteAmount = 30;
    [SerializeField] int bonusBombAmount = 50;
    [SerializeField] int bestScore = 0;
    [SerializeField] int advertUseCounter = 0;
    [SerializeField] int advertUseCounterSetPoint = 1;
    [SerializeField] int advertUseScoreOffset = 50;
    [SerializeField] int gameOverAdvertCounter = 0;
    [SerializeField] int gameOverAdvertMax = 1;
    [SerializeField] bool advertReadyStatus = false;
    private PlayerPref playerPref;
    private PlayerData playerData;

    void OnEnable()
    {
        LoadPlayerPref();
        LoadPlayerData();
        bonusLevelTarget = bonusLevel;
    }


    // score

    public int GetScore()
    {
        return score;
    }
    
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;

        if (score >= bonusLevelTarget)
        {
            AddToBonusScore(bonusAmount);
            bonusLevelTarget = bonusLevelTarget + bonusLevel;
        }
    }

    
    public void RampingScore(int rampValue)
    {
        rampingScore = rampValue;
    }
    
    public int GetRampScore()
    {
        return rampingScore;
    }


    // Advert Use Counter

    public void AdvertUseCounterInc()
    {
        advertUseCounter = advertUseCounter + 1;
    }

    public void AdvertUseCounterReset()
    {
        advertUseCounter = 0;
    }

    public int GetAdvertUseCounter()
    {
        return advertUseCounter;
    }

    public int GetAdvertUseCounterSetPoint()
    {
        return advertUseCounterSetPoint;
    }

    public int GetAdvertUseScoreOffset()
    {
        return advertUseScoreOffset;
    }

    public void GameOverAdvert()
    {
        gameOverAdvertCounter = gameOverAdvertCounter + 1;
    }

    public void GameOverAdvertReset()
    {
        gameOverAdvertCounter = 0;
    }

    public int GetGameOverAdvert()
    {
        return gameOverAdvertCounter;
    }

    public void AdvertStatus(bool adReadyStatus)
    {
        advertReadyStatus = adReadyStatus;
    }

    public bool GetAdvertStatus()
    {
        return advertReadyStatus;
    }


    // Bonus Score

    public int GetBonusScore()
    {
        return bonusScore;
    }

    public void AddToBonusScore(int scoreValue)
    {
        bonusScore += scoreValue;
    }

    public int GetBonusHoldAmount()
    {
        return bonusHoldAmount;
    }

    public int GetBonusNewAmount()
    {
        return bonusNewAmount;
    }

    public int GetBonusDeleteAmount()
    {
        return bonusDeleteAmount;
    }

    public int GetBonusBombAmount()
    {
        return bonusBombAmount;
    }

    public void UseBonus(int useBonusAmount)
    {
        if(bonusScore >= useBonusAmount)
        {
            bonusScore = bonusScore - useBonusAmount;
            if(bonusScore < 0)
            {
                bonusScore = 0;
            }
        }
    }

    // Reset scores

    public void ResetGame()
    {
        score = 0;
        rampingScore = 0;
        bonusScore = 0;
        bonusLevelTarget = bonusLevel;
    }

    // high score

    public void SubmitNewHighScore(int newScore)
    {
        // only update if newScore is higher
        if (newScore > bestScore)
        {
            bestScore = newScore;
            SavePlayerData();
        }
    }

    public int GetHighestScore()
    {
        return bestScore;
    }

    public void ResetHighScore()
    {
        score = 0;
        rampingScore = 0;
        bestScore = 0;
        bonusScore = 0;
        SavePlayerData();

    }

    // Audio control

    public void SetAudioMode(int newValue)
    {
        playerPref.audioMode = newValue;

    }

    public int GetAudioMode()
    {
        return playerPref.audioMode;
    }

    private void LoadPlayerPref()
    {
        playerPref = new PlayerPref();

        //Stores and accesses player preferences between game sessions.
        // HasKey Returns true if key exists in the preferences.
        if (PlayerPrefs.HasKey("audioMode"))
        {
            // get audioMode form PlayePrefs and store in playerPref.audioMode 
            playerPref.audioMode = PlayerPrefs.GetInt("audioMode");
        }
    }



    private void SavePlayerPref()
    {

        // set playerPref.audioMode to PlayePrefs and store in audioMode 
        PlayerPrefs.SetInt("audioMode", playerPref.audioMode);

    }

    public void SavePlayerData()
    {
        // create binary formatter
        BinaryFormatter bf = new BinaryFormatter();
        // create file
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        //copy local data into serilazable object data
        data.highestScore = bestScore;

        // write data (serialize
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // create binary formatter
            BinaryFormatter bf = new BinaryFormatter();
            // create file
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            if (file.Length > 0)
            { 
            // deserialize (read) object into PlayerData cast object and then store into data
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //copy loaded data to local data
            bestScore = data.highestScore;
            }
        }

    }


    void OnDisable()
    {
        ResetGame();
        SavePlayerPref();
        SavePlayerData();
    }
}