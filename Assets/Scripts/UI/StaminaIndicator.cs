using TMPro;
using UnityEngine;

public class StaminaIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _staminaIndicator;

    void Start()
    {
        UpdateStaminaAmount();
    }

    void Update()
    {
        UpdateStaminaAmount();
    }

    public void UpdateStaminaAmount()
    {
        int actualStamina = GameManager.Instance.GetStaminaAmount();
        int maxStamina = GameManager.Instance.GetMaxStaminaAmount();
        _staminaIndicator.text = actualStamina.ToString() + "/" + maxStamina.ToString();
    }
}
