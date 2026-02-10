using Unity.Cinemachine;
using UnityEngine;

public class FollowCameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraManager.FollowRegister(GetComponent<CinemachineCamera>());
    }
    private void OnDisable()
    {
        CameraManager.FollowUnregister(GetComponent<CinemachineCamera>());
    }
}
