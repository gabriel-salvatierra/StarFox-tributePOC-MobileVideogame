using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float _splashDuration = 0.25f;
    [SerializeField] float _fadeInDuration = 0.25f;
    [SerializeField] float _fadeOutDuration = 0.25f;

    [SerializeField] Image logoImage;
    [SerializeField] string _mainMenuName = "Main Menu";

    private void Start()
    {
        StartCoroutine(ShowSplashScreen());
    }

    private IEnumerator ShowSplashScreen()
    {
        if (logoImage != null)
        {
            logoImage.canvasRenderer.SetAlpha(0.0f);
            logoImage.CrossFadeAlpha(1.0f, _fadeInDuration, false);

            yield return new WaitForSeconds(_splashDuration);

            logoImage.CrossFadeAlpha(0.0f, _fadeOutDuration, false);
        }

        yield return new WaitForSeconds(_fadeOutDuration);

        SceneManager.LoadScene(_mainMenuName);
    }
}
