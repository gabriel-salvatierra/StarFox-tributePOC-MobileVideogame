using System.Collections;
using TMPro;
using UnityEngine;

public class MissionComplete : MonoBehaviour
{
    [SerializeField] private string _message = "MISSION COMPLETE";
    [SerializeField] private float _delay = 0.05f;
    private TextMeshProUGUI _tmp;

    void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _tmp.enabled = false;
    }

    public void PlayMessage()
    {
        _tmp.enabled = true;
        StopAllCoroutines();
        StartCoroutine(TypeText(_message));
    }

    private IEnumerator TypeText(string text)
    {
        _tmp.text = "";

        foreach (char c in text)
        {
            _tmp.text += c;
            yield return new WaitForSeconds(_delay);
        }
    }
}
