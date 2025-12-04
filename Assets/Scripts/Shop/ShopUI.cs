using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
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
        }
    }

    private void CheckTwinBlasterTypeA()
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

    private void CheckForceshield()
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


}
