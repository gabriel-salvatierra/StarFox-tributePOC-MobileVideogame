using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsPanelUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _incomingTransmissionGO;
    [SerializeField] private GameObject _fullRewardsGO;
    [SerializeField] private GameObject _someRewardsGO;

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
    }


    void Update()
    {

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
        _fullRewardsGO.SetActive(true);

        _incomingTransmissionGO.SetActive(false);
        _someRewardsGO.SetActive(false);
    }

    public void ShowSomeRewards()
    {
        _someRewardsGO.SetActive(true);

        _fullRewardsGO.SetActive(false);
        _incomingTransmissionGO.SetActive(false);
    }
}
