using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NavMeshSpikePath : MonoBehaviour
{
    [SerializeField] Transform[] _patrolPoints;
    private NavMeshAgent _agent;
    private int _currentIndex = 0;

    private void Awake()
    {
       _agent = GetComponent<NavMeshAgent>();
        if (_patrolPoints.Length > 0)
        {
            _agent.SetDestination(_patrolPoints[_currentIndex].position);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_patrolPoints.Length == 0) return;

        if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _currentIndex = (_currentIndex + 1) % _patrolPoints.Length;
            _agent.SetDestination(_patrolPoints[_currentIndex].position);
        }
    }

}
