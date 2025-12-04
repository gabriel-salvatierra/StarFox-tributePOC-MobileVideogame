using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] private GameObject _forceField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GameManager.Instance.HasForceshield())
        {
            _forceField.SetActive(!_forceField.activeSelf);
        }
    }
}
