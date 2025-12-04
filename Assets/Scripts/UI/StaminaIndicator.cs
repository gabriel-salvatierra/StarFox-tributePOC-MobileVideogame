using TMPro;
using UnityEngine;

public class StaminaIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _staminaIndicator;

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
        int actualStamina = GameManager.Instance.GetStaminaAmount();
        int maxStamina = GameManager.Instance.GetMaxStaminaAmount();
        _staminaIndicator.text = actualStamina.ToString() + "/" + maxStamina.ToString();
    }
}
