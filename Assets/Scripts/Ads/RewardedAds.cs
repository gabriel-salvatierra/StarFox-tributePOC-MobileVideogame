using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string RewardedAdID = "Rewarded_Android";

    public void LoadRewardedAd()
    {
        Advertisement.Load(RewardedAdID, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(RewardedAdID, this);
        LoadRewardedAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log(nameof(OnUnityAdsAdLoaded));
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log(nameof(OnUnityAdsFailedToLoad));
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log(nameof(OnUnityAdsShowClick));
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId != RewardedAdID)
        {
            return;
        }

        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Full rewards");
        }
        else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
        {
            Debug.Log("Some rewards");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Reward & error");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Starting reward ad");
    }
}