using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Main Menu")]
    private const string _mainMenuSceneName = "Main Menu";

    [Header("Panels")]
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _soundPanel;
    [SerializeField] private GameObject _currencyIndicator;
    [SerializeField] private GameObject _staminaIndicator;

    [Header("Currency and Stamina")]
    [SerializeField] TextMeshProUGUI _currencyAmountValue;
    [SerializeField] TextMeshProUGUI _staminaIndicatorValue;

    public static bool IsPaused { get; private set; }

    public void Pause()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _pauseMenuPanel.SetActive(true);
        _currencyIndicator.SetActive(true);
        _staminaIndicator.SetActive(true);

        _pauseButton.SetActive(false);
        _soundPanel.SetActive(false);

        UpdateCurrencyAmount();
        UpdateStaminaAmount();

        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void TogglePause()
    {
        if (IsPaused)
            Resume();
        else
            Pause();
    }

    public void UpdateCurrencyAmount()
    {
        int value = GameManager.Instance.GetCurrencyAmount();
        _currencyAmountValue.text = value.ToString();
    }

    public void UpdateStaminaAmount()
    {
        int actualStamina = GameManager.Instance.GetStaminaAmount();
        int maxStamina = GameManager.Instance.GetMaxStaminaAmount();
        _staminaIndicatorValue.text = actualStamina.ToString() + "/" + maxStamina.ToString();
    }

    public void Resume()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _pauseButton.SetActive(true);

        _pauseMenuPanel.SetActive(false);
        _soundPanel.SetActive(false);
        _currencyIndicator.SetActive(false);
        _staminaIndicator.SetActive(false);

        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void ShowSoundPanel()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _soundPanel.SetActive(true);

        _pauseMenuPanel.SetActive(false);
        _currencyIndicator.SetActive(false);
        _staminaIndicator.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
