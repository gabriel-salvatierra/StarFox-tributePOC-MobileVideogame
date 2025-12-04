using UnityEngine;

public class ButtonAds : MonoBehaviour
{
    public void RequestReward()
    {
        AdsManager.Instance.ShowRewardedAd();
    }
}
