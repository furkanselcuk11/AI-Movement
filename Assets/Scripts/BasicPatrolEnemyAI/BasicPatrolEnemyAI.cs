using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicPatrolEnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float distance;
    [SerializeField] private int wayPointIndex;
    Vector3 target;
    [SerializeField] private bool isRandomTarget;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }    
    void Update()
    {
        if (isRandomTarget)
        {
            if (Vector3.Distance(transform.position, target) < distance)
            {
                RandomWayPointIndex();
                UpdateDestination();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target) < distance)
            {
                IterateWayPointIndex();
                UpdateDestination();
            }
        }
        
    }
    void UpdateDestination()
    {
        target = wayPoints[wayPointIndex].position;
        agent.SetDestination(target);
    }
    void IterateWayPointIndex()
    {
        wayPointIndex++;
        if (wayPointIndex == wayPoints.Length)
        {
            wayPointIndex = 0;
        }
    }
    void RandomWayPointIndex()
    {
        wayPointIndex = Random.Range(0,wayPoints.Length);
    }
}
