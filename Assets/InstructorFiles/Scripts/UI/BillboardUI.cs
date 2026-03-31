using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main; // Ensure your main camera is tagged as "MainCamera"
    }

    void LateUpdate()
    {
        if (_mainCamera != null)
        {
            //transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
            //_mainCamera.transform.rotation * Vector3.up);
            transform.forward = _mainCamera.transform.forward;
        }
    }
}
