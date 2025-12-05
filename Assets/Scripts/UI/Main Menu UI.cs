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

    [Header("Level Indexes")]
    [SerializeField] private int _level1 = 4;
    [SerializeField] private TextMeshProUGUI _level1Stamina;
    [SerializeField] private int _level2 = 5;
    [SerializeField] private TextMeshProUGUI _level2Stamina;

    [Header("Game Values Button")]
    [SerializeField] private GameObject _restoreGameValuesButton;

    private ShopUI _shopUI;

    void Start()
    {
        _shopUI = GetComponent<ShopUI>();
        ShowMainMenu();
        CheckGameValuesButton();
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

    public void PlayLevel1()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck,1f);
        SceneManager.LoadScene(_level1);
        if (int.TryParse(_level1Stamina.text, out int staminaConsumption))
        {
            GameManager.Instance.ModifyStaminaAmount(-staminaConsumption);
        }
    }

    public void PlayLevel2()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.GoodLuck,1f);
        SceneManager.LoadScene(_level2);
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
