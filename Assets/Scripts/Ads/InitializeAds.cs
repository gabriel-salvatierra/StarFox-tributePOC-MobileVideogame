using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    // Acá ponen su ID, el que les generó unity con su usuario.
    private const string AndroidID = "5996740";
    [SerializeField]
    private bool testMode = true;

    private void Awake()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(AndroidID, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialization sucess.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Initialization failure.");
    }
}
