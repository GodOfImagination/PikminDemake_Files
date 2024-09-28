using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterControl : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform _target;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_target != null)
            _agent.SetDestination(_target.position);
    }
}
