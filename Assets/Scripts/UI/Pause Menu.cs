using System.Collections;
using System.Collections.Generic;
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

    public void Pause()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _pauseMenuPanel.SetActive(true);
        _currencyIndicator.SetActive(true);
        _staminaIndicator.SetActive(true);

        _pauseButton.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void Resume()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _pauseButton.SetActive(true);

        _pauseMenuPanel.SetActive(false);
        _soundPanel.SetActive(false);
        _currencyIndicator.SetActive(false);
        _staminaIndicator.SetActive(false);
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
