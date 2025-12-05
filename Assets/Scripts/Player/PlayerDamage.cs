using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MusicManager;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int _playerCurrentHealth;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeStrength = 0.15f;
    [SerializeField] private float _nextSceneDelay = 20f;
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private const string _mainMenuSceneName = "Main Menu";

    private Material _material;
    private Color _originalColor;
    private bool _isFlashing = false;

    private bool isShaking;

    private void Start()
    {
        _playerCurrentHealth = GameManager.Instance.GetPlayerMaxHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Collision Enemy");
            TakeDamage(2);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            Debug.Log("Collision Object");
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.Hit);
        _playerCurrentHealth -= amount;
        Debug.Log("Player hit! Health = " + _playerCurrentHealth);
        SmallShake();
        //Flash();

        if (_playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Flash()
    {
        if (!_isFlashing)
            StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _isFlashing = true;
        _material.SetColor("_MainColor", Color.red);

        yield return new WaitForSeconds(flashDuration);

        _material.SetColor("_MainColor", Color.white);
        _isFlashing = false;
    }


    private void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        SFXManager.Instance.PlaySFX(SFXManager.SFXCategoryType.Explosion);

        // Destroy boss object; TO DO explosion
        _playerRenderer.enabled = false;
        GetComponentInChildren<Collider>().enabled = false;

        yield return new WaitForSeconds(_nextSceneDelay);

        SceneManager.LoadScene(_mainMenuSceneName);

        Destroy(gameObject);
    }

    public void SmallShake()
    {
        if (!isShaking)
            StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        isShaking = true;
        Vector3 startPos = transform.position;

        float timer = 0f;

        while (timer < shakeDuration)
        {
            Vector3 offset = new Vector3(
                Random.Range(-shakeStrength, shakeStrength),
                0f,
                Random.Range(-shakeStrength, shakeStrength)
            );

            transform.position = startPos + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
        isShaking = false;
    }
}
