using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private Image _nextLevelImageButton;
    private const string _mainMenuSceneName = "Main Menu";

    [Header("Prices")]
    [SerializeField] TextMeshProUGUI _twinBlasterTypeA;
    [SerializeField] TextMeshProUGUI _forceShield;

    [Header("Disable")]
    [SerializeField] private GameObject _twinBlasterTypeAPanel;
    [SerializeField] private GameObject _twinBlasterTypeAButton;
    [SerializeField] private GameObject _forceshieldPanel;
    [SerializeField] private GameObject _forceshieldButton;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        CheckTwinBlasterTypeA();
        CheckForceshield();
    }

    public void BuyTwinBlasterTypeA()
    {
        int price = int.Parse(_twinBlasterTypeA.text);
        if (price <= _gameManager.GetCurrencyAmount() && !_gameManager.HasTwinBlasterTypeA())
        {
            _gameManager.UnlockTwinBlasterTypeA();
            _gameManager.ModifyCurrencyAmount(-price);

            _twinBlasterTypeAPanel.SetActive(true);
            _twinBlasterTypeAButton.SetActive(false);

            SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.TwinBlaster);
        }
    }

    public void BuyForceShield()
    {
        int price = int.Parse(_forceShield.text);
        if (price <= _gameManager.GetCurrencyAmount() && !_gameManager.HasForceshield())
        {
            _gameManager.UnlockForceshield();
            _gameManager.ModifyCurrencyAmount(-price);

            _forceshieldPanel.SetActive(true);
            _forceshieldButton.SetActive(false);

            SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.WeaponPowerUp);
        }
    }

    public void CheckTwinBlasterTypeA()
    {
        if (_gameManager.HasTwinBlasterTypeA())
        {
            _twinBlasterTypeAPanel.SetActive(true);
            _twinBlasterTypeAButton.SetActive(false);
        }
        else
        {
            _twinBlasterTypeAPanel.SetActive(false);
            _twinBlasterTypeAButton.SetActive(true);
        }
    }

    public void CheckForceshield()
    {
        if (_gameManager.HasForceshield())
        {
            _forceshieldPanel.SetActive(true);
            _forceshieldButton.SetActive(false);
        }
        else
        {
            _forceshieldPanel.SetActive(false);
            _forceshieldButton.SetActive(true);
        }
    }

    public void NextMission()
    {
        if (_gameManager.GetStaminaAmount() > 0)
        {
            SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress,0.5f);
            _gameManager.ModifyStaminaAmount(-1);
            SceneManager.LoadScene(_gameManager.GetNextLevelAfterShop());
        }
        else
        {
            _nextLevelImageButton.color = Color.black;
        }
    }

    public void LoadMainMenu()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress, 0.5f);
        SceneManager.LoadScene(_mainMenuSceneName);
    }

}
