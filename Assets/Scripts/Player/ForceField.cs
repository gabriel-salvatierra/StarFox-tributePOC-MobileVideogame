using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] private GameObject _forceField; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _forceField.SetActive(!_forceField.activeSelf);
        }
    }
}
