using UnityEngine;
using Unity.Cinemachine;

public class LevelCameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraManager.LevelRegister(GetComponent<CinemachineCamera>());
    }
    private void OnDisable()
    {
        CameraManager.LevelUnregister(GetComponent<CinemachineCamera>());
    }
}
