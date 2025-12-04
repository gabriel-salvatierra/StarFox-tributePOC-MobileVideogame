using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _soundPanel;

    [Header("Level Indexes")]
    [SerializeField] private int _level1 = 2;
    [SerializeField] private int _level2 = 3;

    [Header("Game Values Button")]
    [SerializeField] private GameObject _restoreGameValuesButton;

    void Start()
    {
        ShowMainMenu();
        CheckGameValuesButton();
    }

  public void CheckGameValuesButton()
    {
        if (GameManager.Instance.GetShowRestoreGameValues())
        {
            _restoreGameValuesButton.SetActive(true);
        } else
        {
            _restoreGameValuesButton.SetActive(false);
        }
    }

    public void PlayLevel1()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck);
        SceneManager.LoadScene(_level1);
        GameManager.Instance.RestoreGameValues();
    }

    public void PlayLevel2()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck);
        SceneManager.LoadScene(_level2);
        GameManager.Instance.RestoreGameValues();
    }

    public void ShowMainMenu()
    {
        _mainMenuPanel.SetActive(true);

        _levelSelectPanel.SetActive(false);
        _shopPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _mainMenuPanel.SetActive(true);

        _levelSelectPanel.SetActive(false);
        _shopPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _levelSelectPanel.SetActive(true);

        _mainMenuPanel.SetActive(false);
        _shopPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void ShowShop()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _shopPanel.SetActive(true);

        _levelSelectPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void ShowSoundPanel()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress);
        _soundPanel.SetActive(true);

        _shopPanel.SetActive(false);
        _levelSelectPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
    }

    public void RestoreGameValues()
    {
        GameManager.Instance.RestoreGameValues();
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
