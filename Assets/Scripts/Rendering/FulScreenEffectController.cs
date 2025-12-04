using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FulScreenEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _speedVignette;
    [SerializeField] private Material _material;

    private void Start()
    {
        _speedVignette.SetActive(false);
    }

    public void ToggleSpeedVignette(bool toggle)
    {
        _speedVignette.SetActive(toggle);
    }

}
