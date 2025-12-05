using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    private DateTime _nextStaminaTime;
    private DateTime _lastStaminaTime;

    [SerializeField] private float _timerToRecharge = 10f;
    [SerializeField] private TextMeshProUGUI _timerText;

    private bool _recharging;
    private TimeSpan notifTimer;
    private int _id;

    private const string NEXT_STAMINA_TIME = "NextStaminaTime";
    private const string LAST_STAMINA_TIME = "LastStaminaTime";

    private void Start()
    {
        LoadGame();

        StartCoroutine(ChargingStamina());

        if (CurrentStamina < MaxStamina)
        {
            notifTimer = _nextStaminaTime - DateTime.Now;
            DisplayNotif();
        }
    }

    private int CurrentStamina => GameManager.Instance.GetStaminaAmount();
    private int MaxStamina => GameManager.Instance.GetMaxStaminaAmount();

    private IEnumerator ChargingStamina()
    {
        UpdateTimer();
        _recharging = true;

        while (CurrentStamina < MaxStamina)
        {
            DateTime current = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;

            bool added = false;

            while (current > nextTime)
            {
                if (CurrentStamina >= MaxStamina)
                    break;

                GameManager.Instance.ModifyStaminaAmount(1);
                added = true;

                DateTime timeToAdd = (_lastStaminaTime > nextTime) ? _lastStaminaTime : nextTime;
                nextTime = timeToAdd.AddSeconds(_timerToRecharge);
            }

            if (added)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;
            }

            UpdateTimer();
            SaveGame();

            yield return new WaitForEndOfFrame();
        }

        MobileNotificationManager.Instance.CancelNotification(_id);
        _recharging = false;
    }

    public bool HasEnoughStamina(int amount)
    {
        return CurrentStamina - amount >= 0;
    }

    public void UseStamina(int amount)
    {
        if (!HasEnoughStamina(amount))
        {
            Debug.Log("No stamina!");
            return;
        }

        GameManager.Instance.ModifyStaminaAmount(-amount);
        MobileNotificationManager.Instance.CancelNotification(_id);
        DisplayNotif();

        if (!_recharging)
        {
            _nextStaminaTime = DateTime.Now.AddSeconds(_timerToRecharge);
            StartCoroutine(ChargingStamina());
        }
    }

    private void DisplayNotif()
    {
        int missingStamina = MaxStamina - CurrentStamina;

        DateTime fireDuration =
            DateTime.Now.AddSeconds(missingStamina * _timerToRecharge + 1 + notifTimer.TotalSeconds);

        _id = MobileNotificationManager.Instance.DisplayNotification(
            "VUELVEEEEEEEE!!!",
            "Te extraño, ven a jugar",
            IconSelecter.myicon_0,
            IconSelecter.myicon_1,
            fireDuration);
    }

    private void UpdateTimer()
    {
        if (CurrentStamina >= MaxStamina)
        {
            _timerText.text = "Full stamina!";
            return;
        }

        notifTimer = _nextStaminaTime - DateTime.Now;
        _timerText.text = $"{notifTimer.Minutes:00} : {notifTimer.Seconds:00}";
    }

    private void SaveGame()
    {
        PlayerPrefs.SetString(NEXT_STAMINA_TIME, _nextStaminaTime.ToString());
        PlayerPrefs.SetString(LAST_STAMINA_TIME, _lastStaminaTime.ToString());
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString(NEXT_STAMINA_TIME));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString(LAST_STAMINA_TIME));
    }

    private DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now;

        return DateTime.Parse(date);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
