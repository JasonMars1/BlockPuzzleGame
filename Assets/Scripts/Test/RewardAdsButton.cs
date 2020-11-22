using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardAdsButton : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "3523736";
//#elif UNITY_ANDROID
#else
    private string gameId = "3523737";
#endif

    Button GameAdButton;
    public string myPlacementId = "rewardedVideo";
    public GameState gameState; // scriptable object
    [SerializeField] int adBonusAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        GameAdButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        GameAdButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (GameAdButton) GameAdButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            GameAdButton.interactable = true;
        }
    }

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
        // remove listener after advert done or error
        Advertisement.RemoveListener(this);
        GameAdButton.onClick.RemoveListener(ShowRewardedVideo);
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
        // remove listener after advert done or error
        Advertisement.RemoveListener(this);
        GameAdButton.onClick.RemoveListener(ShowRewardedVideo);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

}
