using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _soundPanel;

    [Header("Levels")]
    [SerializeField] private int _level1Index = 4;
    [SerializeField] private bool _level1Completed;
    [SerializeField] private TextMeshProUGUI _level1Stamina;
    [SerializeField] private GameObject _level1DisabledPanel;

    [SerializeField] private int _level2Index = 5;
    [SerializeField] private bool _level2Completed;
    [SerializeField] private TextMeshProUGUI _level2Stamina;
    [SerializeField] private GameObject _level2DisabledPanel;

    [Header("Game Values Button")]
    [SerializeField] private GameObject _restoreGameValuesButton;

    private ShopUI _shopUI;

    void Start()
    {
        _shopUI = GetComponent<ShopUI>();
        ShowMainMenu();
        CheckGameValuesButton();
        CheckCompletedLevels();
    }

    public void CheckGameValuesButton()
    {
        if (GameManager.Instance.GetShowRestoreGameValues())
        {
            _restoreGameValuesButton.SetActive(true);
        }
        else
        {
            _restoreGameValuesButton.SetActive(false);
        }
    }

    public void CheckCompletedLevels()
    {
        _level1Completed = GameManager.Instance.IsLevelCompleted(1);
        _level2Completed = GameManager.Instance.IsLevelCompleted(2);

        if (_level1Completed) { _level1DisabledPanel.SetActive(true); }
        else
        {
            _level1DisabledPanel.SetActive(false);
        }

        if (_level2Completed) { _level2DisabledPanel.SetActive(true); }
        else
        {
            _level2DisabledPanel.SetActive(false);
        }
    }

    public void PlayLevel1()
    {
        if (_level1Completed) { return; }

        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck, 1f);
        SceneManager.LoadScene(_level1Index);
        if (int.TryParse(_level1Stamina.text, out int staminaConsumption))
        {
            GameManager.Instance.ModifyStaminaAmount(-staminaConsumption);
        }
    }

    public void PlayLevel2()
    {
        if (_level2Completed) { return; }

        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck, 1f);
        SceneManager.LoadScene(_level2Index);
        if (int.TryParse(_level2Stamina.text, out int staminaConsumption))
        {
            GameManager.Instance.ModifyStaminaAmount(-staminaConsumption);
        }
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
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress, 0.5f);
        _mainMenuPanel.SetActive(true);

        _levelSelectPanel.SetActive(false);
        _shopPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        CheckCompletedLevels();

        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress, 0.5f);
        _levelSelectPanel.SetActive(true);

        _mainMenuPanel.SetActive(false);
        _shopPanel.SetActive(false);
        _soundPanel.SetActive(false);
    }

    public void ShowShop()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress, 0.5f);
        _shopPanel.SetActive(true);

        _levelSelectPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        _soundPanel.SetActive(false);

        _shopUI.CheckTwinBlasterTypeA();
        _shopUI.CheckForceshield();
    }

    public void ShowSoundPanel()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.ButtonPress, 0.5f);
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
