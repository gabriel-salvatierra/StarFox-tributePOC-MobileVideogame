using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    public void SmallShake()
    {
        StartCoroutine(SmallShakeRoutine());
    }

    private IEnumerator SmallShakeRoutine()
    {
        Vector3 originalPos = transform.localPosition;
        float duration = 0.15f;
        float strength = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * strength;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
