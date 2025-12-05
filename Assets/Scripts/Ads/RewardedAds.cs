using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private AdsPanelUI _adsPanelUI;

    private const string RewardedAdID = "Rewarded_Android";
    public bool isReady { get; private set; }

    public void ShowAd()
    {
        if (!isReady)
        {
            Debug.Log("Ad not ready yet!");
            return;
        }

        Advertisement.Show(RewardedAdID, this);
    }

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
        if (placementId == RewardedAdID)
        {
            isReady = true;
            Debug.Log("Rewarded ad is READY.");
        }
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
            _adsPanelUI.GiveFullReward();
        }
        else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
        {
            Debug.Log("Some rewards");
            _adsPanelUI.GiveSomeReward();
        }
        else
        {
            Debug.Log("Error");
            _adsPanelUI.GiveFullReward();
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