using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string InterstitialAdID = "Interstitial_Android";

    public void LoadInterstitialAd()
    {
        Advertisement.Load(InterstitialAdID, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(InterstitialAdID, this);
        LoadInterstitialAd();
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
        Debug.Log(nameof(OnUnityAdsShowComplete));

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log(nameof(OnUnityAdsShowFailure));

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log(nameof(OnUnityAdsShowStart));

    }
}
