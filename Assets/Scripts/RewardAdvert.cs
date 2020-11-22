using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


//IUnityAdsListener
//An interface for handling various states of an advert. listener methods ready start error finish.


public class RewardAdvert : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3523736";
//#elif UNITY_ANDROID
#else
    private string gameId = "3523737";
#endif
        
    public string myPlacementId = "rewardedVideo";
    public GameState gameState; // scriptable object
    [SerializeField] int adBonusAmount = 10;
    [SerializeField] bool advertIsReadyToBeUsed = false;

    // Start is called before the first frame update
    void Start()
    {

        // Set interactivity to be dependent on the Placement’s status:
        advertIsReadyToBeUsed = Advertisement.IsReady(myPlacementId);

        // Initialize the Ads listener and service: to this script instance
        Advertisement.AddListener(this);
        // Initializes the ads service, with a specified Game ID, test mode status
        Advertisement.Initialize(gameId, true);
    }

    void Update()
    {
        // Set interactivity to be dependent on the Placement’s status:
        advertIsReadyToBeUsed = Advertisement.IsReady(myPlacementId);
        //update gamestate with advert is ready status for block button images
        gameState.AdvertStatus(advertIsReadyToBeUsed);
    }

    // called from gameover popup button
    public void GameRewardAdBtn()
    {
        if (advertIsReadyToBeUsed)
        {
            ShowRewardedVideo();
        }
    }

    //called from game reward button
    public void GameOverRewardAdBtn()
    {
        if (advertIsReadyToBeUsed)
        {
            // udpate gamestate to log gameover advert played
            gameState.GameOverAdvert();
            ShowRewardedVideo();
        }
    }


    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        //Shows content in the specified Placement, if it is ready.
        Advertisement.Show(myPlacementId);

    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            advertIsReadyToBeUsed = true;
        }
    }

    //ShowResult
    //The enumerated states of the end-user’s interaction with the ad. The SDK passes this value to the OnUnityAdsDidFinish callback method when the ad completes.
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            gameState.AddToBonusScore(adBonusAmount);
            gameState.AdvertUseCounterInc();


        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
        
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
        
    }

    void OnDestroy()
    {
        // remove listener on scene change
        Advertisement.RemoveListener(this);

    }

}
