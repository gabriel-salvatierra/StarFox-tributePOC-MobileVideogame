using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

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
        if (missionCompleteUI != null)
            missionCompleteUI.PlayMessage();

        Destroy(gameObject);
    }
}
