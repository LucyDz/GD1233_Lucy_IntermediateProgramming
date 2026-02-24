using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshPlayerChaser : MonoBehaviour
{
    [SerializeField] private PlayerTargetProvider _targetProvider;
    [SerializeField] private NavMeshAgent _agent;

    private Vector3 _playerPos;

    // public geters for agent related information
    public Vector3 Velocity => _agent.velocity;
    public bool HasPath => _agent.hasPath;

    public void SetDestination(Vector3 targetPos)
    {
        //Best practice: don't spam SetDestination every frame if you don't need to.

        _agent?.SetDestination(_targetProvider.GetTargetPosition());
    }

    
    public void Update()
    {
        _agent?.SetDestination(_targetProvider.GetTargetPosition());
    }
    public void Stop()
    {
        _agent?.ResetPath();
    }

}
