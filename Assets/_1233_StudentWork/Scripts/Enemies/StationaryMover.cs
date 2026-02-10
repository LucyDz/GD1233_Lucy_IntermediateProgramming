using UnityEngine;

public class StationaryMover : MonoBehaviour, IMover
{
    public Vector3 Velocity => Vector3.zero;
    public float RemainingDistance => 0f;
    public bool IsAtDestination => true;

    public void SetDestination(Vector3 destination)
    {
        //nothing ever happens//
    }
    public void Stop()
    {
        //nothing ever happens//
    }
    public void Resume()
    {
        //nothing ever happens//
    }
    public void SetEnabled(bool value)
    {
        //nothing ever happens//
    }
}
