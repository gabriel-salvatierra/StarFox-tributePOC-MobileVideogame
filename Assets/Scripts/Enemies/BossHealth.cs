using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MusicManager;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Currency Value")]
    [SerializeField] private int _currencyOnDefeat = 15;

    [Header("On Defeat")]
    private const string _sceneOnDefeat = "Main Menu";
    [SerializeField] private float _nextSceneDelay = 15f;

    [Header("Flash Effect")]
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Mission Complete UI")]
    [SerializeField] private MissionComplete missionCompleteUI;

    private Material _material;
    private Color _originalColor;
    private bool _isFlashing = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        _material = enemyRenderer.material;
        _originalColor = _material.color;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        FlashWhite();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void FlashWhite()
    {
        if (!_isFlashing)
            StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _isFlashing = true;
        _material.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        _material.color = _originalColor;
        _isFlashing = false;
    }

    private void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        // Play UI message
        if (missionCompleteUI != null)
            missionCompleteUI.PlayMessage();

        // Give player the currency reward
        GameManager.Instance.ModifyCurrencyAmount(_currencyOnDefeat);

        // Play Music and SFX
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.Explosion);
        MusicManager.Instance.PlayTrack(MusicTrack.LevelClear);

        // Destroy boss object; TO DO explosion
        enemyRenderer.enabled = false;

        yield return new WaitForSeconds(_nextSceneDelay);

        SceneManager.LoadScene(_sceneOnDefeat);
    
        Destroy(gameObject);
    }
}
