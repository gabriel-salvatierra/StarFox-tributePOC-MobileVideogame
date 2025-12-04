using UnityEngine;
using UnityEngine.VFX;

public class VFXBoostColor : MonoBehaviour
{
    private VisualEffect _vfx;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
    }

    public void SetEvent(string eventName)
    {
        _vfx.SendEvent(eventName);
    }
}
