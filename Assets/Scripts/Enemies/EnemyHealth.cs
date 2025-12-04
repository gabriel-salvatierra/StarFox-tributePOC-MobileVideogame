using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Flash Effect")]
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private float flashDuration = 0.1f;

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
        // TO DO: explosion
        Destroy(gameObject);
    }
}