using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private int _blinkCount = 4;
    [SerializeField] private float _interval = 1f;

    private void OnEnable()
    {
        _target.SetActive(true);
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _target.SetActive(false);
            yield return new WaitForSeconds(_interval);

            _target.SetActive(true);
            yield return new WaitForSeconds(_interval);
        }

        _target.SetActive(false);
    }
}
