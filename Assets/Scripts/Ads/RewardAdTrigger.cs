using System.Collections;
using UnityEngine;

public class RewardAdTrigger : MonoBehaviour
{
    private void Start()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

}
