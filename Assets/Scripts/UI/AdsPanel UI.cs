using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsPanelUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _incomingTransmissionGO;
    [SerializeField] private GameObject _fullRewardsGO;
    [SerializeField] private GameObject _someRewardsGO;

    [Header("Rewards")]
    [SerializeField] private RewardedAds _rewardedAds;

    [Header("Full Rewards")]
    [SerializeField] TextMeshProUGUI _staminaFullTextAmount;
    [SerializeField] TextMeshProUGUI _currencyFullTextAmount;

    [Header("Some Rewards")]
    [SerializeField] TextMeshProUGUI _staminaSomeTextAmount;
    [SerializeField] TextMeshProUGUI _currencySomeTextAmount;

    private string _shopSceneName = "Shop";
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        ShowIncomingTransmission();
        _rewardedAds.AssignAdsUI();
    }

    public void WatchAd()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

    public void GoToShop()
    {
        SceneManager.LoadScene(_shopSceneName);
    }

    public void GiveFullReward()
    {
        Debug.Log("Give Full Rewards");
        int staminaWon = int.Parse(_staminaFullTextAmount.text);
        int currencyWon = int.Parse(_currencyFullTextAmount.text);

        _gameManager.ModifyStaminaAmount(staminaWon);
        _gameManager.ModifyCurrencyAmount(currencyWon);

        ShowFullRewards();
    }

    public void GiveSomeReward()
    {
        int staminaWon = int.Parse(_staminaSomeTextAmount.text);
        int currencyWon = int.Parse(_currencySomeTextAmount.text);

        _gameManager.ModifyStaminaAmount(staminaWon);
        _gameManager.ModifyCurrencyAmount(currencyWon);

        ShowSomeRewards();
    }

    public void ShowIncomingTransmission()
    {
        _incomingTransmissionGO.SetActive(true);

        _fullRewardsGO.SetActive(false);
        _someRewardsGO.SetActive(false);
    }

    public void ShowFullRewards()
    {
        Debug.Log("Show Full Rewards");
        _fullRewardsGO.SetActive(true);

        _incomingTransmissionGO.SetActive(false);
        _someRewardsGO.SetActive(false);
    }

    public void ShowSomeRewards()
    {
        Debug.Log("Give Some Rewards");
        _someRewardsGO.SetActive(true);

        _fullRewardsGO.SetActive(false);
        _incomingTransmissionGO.SetActive(false);
    }
}
