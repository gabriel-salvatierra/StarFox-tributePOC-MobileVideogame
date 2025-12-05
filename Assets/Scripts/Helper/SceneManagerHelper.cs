using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    [SerializeField] private int _nextSceneAfterShop;
    [SerializeField] private bool _forceSerializedValue = false;

    void Start()
    {
        if (!_forceSerializedValue)
        {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SetNextLevelAfterShop(currentSceneBuildIndex + 1);

            // For Debug
            _nextSceneAfterShop = GameManager.Instance.GetNextLevelAfterShop();
        }
        else
        {
            GameManager.Instance.SetNextLevelAfterShop(_nextSceneAfterShop);
        }

    }


}
