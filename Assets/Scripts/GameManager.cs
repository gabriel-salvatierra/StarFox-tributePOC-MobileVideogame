using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern: una sola instancia de la clase (si existe una se usa la misma)
    private static GameManager _instance;

    // Player reference property
    private GameObject _player;
    public GameObject Player => _player != null ? _player : throw new Exception("Player not set yet!");
    public int SelectedSkin { get; private set; }


    // Para pasar info entre escenas
    [SerializeField] static int _initalLives = 3;
    [SerializeField] static int _actualLives = 3;
    [SerializeField] static int _currencyAmount;
    [SerializeField] static int _staminaAmount;
    [SerializeField] static int _maxStaminaAmount;
    //defaults
    [SerializeField] static int _currencyDefault = 0;
    [SerializeField] static int _staminaDefault = 10;
    [SerializeField] static int _maxStaminaDefault = 10;
    [SerializeField] static bool _showRestoreGameValues = true;


    // Keys para PlayerPrefs
    private const string CurrencyKey = "Currency";
    private const string StaminaKey = "Stamina";
    private const string MaxStaminaKey = "MaxStamina";

    // SceneManagement
    [SerializeField] string _sceneAfterGameOver = "SplashScreen";

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void OnEnable()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            SelectedSkin = PlayerPrefs.GetInt("SelectedSkin", 1); // skin default
                                                                  // Load persistent values OR defaults
            _currencyAmount = PlayerPrefs.GetInt(CurrencyKey, _currencyDefault);
            _staminaAmount = PlayerPrefs.GetInt(StaminaKey, _staminaDefault);
            _maxStaminaAmount = PlayerPrefs.GetInt(MaxStaminaKey, _maxStaminaDefault);
        }
    }

    public void ProcessPlayerDeath(int num)
    {
        if (_actualLives >= 1)
        {
            //ModifyGoosetavLivesAmount(num);
        }
        else
        {
            SceneManager.LoadScene(_sceneAfterGameOver);
            RestoreGameValues();
        }
    }

    //public int GetLivesAmount() { return _actualLives; }
    public int GetCurrencyAmount() { return _currencyAmount; }
    public int GetStaminaAmount() { return _staminaAmount; }
    public int GetMaxStaminaAmount() { return _maxStaminaAmount; }
    public bool GetShowRestoreGameValues() { return _showRestoreGameValues; }


    /*public void ModifyLivesAmount(int amount)
    {
        if (amount > 0)
        {
            _actualLives++;
        }
        else
        {
            _actualLives--;
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }*/

    public void ModifyCurrencyAmount(int amount)
    {
        _currencyAmount += amount;
        PlayerPrefs.SetInt(CurrencyKey, _currencyAmount);
        PlayerPrefs.Save();
    }

    public void ModifyStaminaAmount(int amount)
    {
        _staminaAmount = Mathf.Clamp(_staminaAmount + amount, 0, _maxStaminaAmount);
        PlayerPrefs.SetInt(StaminaKey, _staminaAmount);
        PlayerPrefs.Save();
    }

    public void RestoreGameValues()
    {
        Debug.Log("Game values set to default");
        _currencyAmount = 0;
        _staminaAmount = _maxStaminaAmount;

        PlayerPrefs.SetInt(CurrencyKey, _currencyAmount);
        PlayerPrefs.SetInt(StaminaKey, _staminaAmount);
        PlayerPrefs.Save();
    }

    public void LoadNextScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void SetPlayerReference(GameObject playerRef)
    {
        _player = playerRef;
    }

    public void SetSkin(int skinIndex)
    {
        SelectedSkin = skinIndex;
        PlayerPrefs.SetInt("SelectedSkin", skinIndex);
        PlayerPrefs.Save();
    }

}
