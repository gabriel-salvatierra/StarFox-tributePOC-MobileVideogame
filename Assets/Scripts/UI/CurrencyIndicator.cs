using TMPro;
using UnityEngine;

public class CurrencyIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currencyAmount;

    void Start()
    {
        UpdateCurrencyAmount();
    }

    void Update()
    {
        UpdateCurrencyAmount();
    }

    public void UpdateCurrencyAmount()
    {
        int value = GameManager.Instance.GetCurrencyAmount();
        _currencyAmount.text = value.ToString();
    }
}
