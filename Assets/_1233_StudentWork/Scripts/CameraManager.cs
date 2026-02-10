using UnityEngine;
using Unity.Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;

public class CameraManager : Singleton<CameraManager>
{
   static List<CinemachineCamera> followcameras = new List<CinemachineCamera>();
   static List<CinemachineCamera> levelcameras = new List<CinemachineCamera>();
    public static CinemachineCamera ActiveCamera = null;
    private bool _followmode = true;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }
    //switches camera priorities aka switches view
    public void SwitchCamera()
    {
        _followmode = !_followmode;
        
        foreach (CinemachineCamera cam in followcameras)
        {
            if (_followmode == true)
            {
                cam.Priority = 10;
            }
            else cam.Priority = 0;
        }
        foreach (CinemachineCamera cam in levelcameras)
        {
            if (_followmode == false)
            {
                cam.Priority = 10;
            }
            else cam.Priority = 0;
        }
    }
    //adds/unadds cameras to the list
    public static void LevelRegister(CinemachineCamera camera)
    {
        levelcameras.Add(camera);
    }

    public static void LevelUnregister(CinemachineCamera camera)
    {
        levelcameras.Remove(camera);
    }

    public static void FollowRegister(CinemachineCamera camera)
    {
        followcameras.Add(camera);
    }

    public static void FollowUnregister(CinemachineCamera camera)
    {
        followcameras.Remove(camera);
    }
}
