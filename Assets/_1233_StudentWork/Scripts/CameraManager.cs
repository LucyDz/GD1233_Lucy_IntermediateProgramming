using UnityEngine;
using Unity.Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
   static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static CinemachineCamera ActiveCamera = null;

    
    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }
    //switches camera priorities aka switches view
    public static void SwitchCamera(CinemachineCamera newCamera)
    {
        newCamera.Priority = 1;
        ActiveCamera = newCamera;

        foreach (CinemachineCamera cam in cameras)
        {
            if (cam != newCamera)
            {
                cam.Priority = 0;
            }
        }
    }
    //adds/unadds cameras to the list
    public static void Register(CinemachineCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineCamera camera)
    {
        cameras.Remove(camera);
    }
}
