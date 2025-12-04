using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    [SerializeField] private int _nextSceneAfterShop;

    void Start()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.SetNextLevelAfterShop(currentSceneBuildIndex + 1);

        // For Debug
        _nextSceneAfterShop = GameManager.Instance.GetNextLevelAfterShop();
    }


}
