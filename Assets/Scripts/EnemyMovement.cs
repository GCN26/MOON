using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    public float minPlayerDist;
    public float maxPlayerDist;

    [Header("Detection")]
    public float visionRange;
    public LayerMask visionBlockers;
    public Transform eyes;
    public string playerTag;
    public bool targetSpotted;
    public float playerDist;

    [Header("Combat")]
    public float combatCooldown;
    private float combatCooldownCounter = 0;
    [Header("Idling Time")]
    public float idleWaitTime;
    private float idleWaitTimeCounter = 0;
    [Header("Patrol")]
    public float patrolRange;
    private Vector3 patrolDestination;
    public float minPatrolDestinationDistance;
    public Vector3 startPos;

    public float maxPatrolLen,distFromStart;
    

    public enum EnemyStates { Idle, Patrol, Chase, Attack, Dead };
    public EnemyStates _currentState;
    public EnemyStates currentState
    {
        get { return _currentState; }
        set
        {
            if (value == EnemyStates.Idle)
            {
                idleWaitTimeCounter = 0;
            }
            else if (value == EnemyStates.Patrol)
            {
                patrolDestination = this.transform.position + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
            }
            else if (value == EnemyStates.Chase)
            {
            }
            else if (value == EnemyStates.Attack)
            {
                combatCooldownCounter = 0;
            }
            _currentState = value;
        }
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Debug.DrawRay(eyes.transform.position, transform.TransformDirection(Vector3.forward) * visionRange, Color.blue);

        switch (currentState)
        {
            case EnemyStates.Idle:
                Idle(); break;
            case EnemyStates.Patrol:
                Patrol(); break;
            case EnemyStates.Chase:
                Chase(); break;
            case EnemyStates.Attack:
                Attack(); break;
            case EnemyStates.Dead:
                Dead(); break;
            default:
                Debug.Log("State not recognized"); break;
        }

        playerDist = Vector3.Distance(this.transform.position, target.position);
    }

    private bool IsTargetVisible()
    {

        Debug.DrawRay(eyes.position, (target.position - eyes.position).normalized * visionRange, Color.red, 0.01f);
        
        if (Physics.Raycast(eyes.position,(target.position - eyes.position).normalized, out RaycastHit hit, visionRange, visionBlockers))
        {
            //maybe replace with player tag
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    private void Idle()
    {
        idleWaitTimeCounter += Time.deltaTime;
        if (IsTargetVisible())
        {
            currentState = EnemyStates.Chase;
            return;
        }
        else if (idleWaitTimeCounter >= idleWaitTime)
        {
            currentState = EnemyStates.Patrol;
            return;
        }
    }
    private void Patrol()
    {
        
        distFromStart = Vector3.Distance(this.transform.position, startPos);
        if (distFromStart > maxPatrolLen)
        {
            agent.destination = startPos;
        }
        else
        {
            agent.destination = patrolDestination;
        }

        if (IsTargetVisible())
        {
            currentState = EnemyStates.Chase;
            return;
        }
        else if (Vector3.Distance(this.transform.position, patrolDestination) >= minPatrolDestinationDistance)
        {
            currentState = EnemyStates.Idle;
            return;
        }
        //add a timer if it takes too long due to being stuck
    }
    private void Chase()
    {
        agent.destination = target.position;
        if (playerDist > maxPlayerDist)
        {
            agent.destination = startPos;
            currentState = EnemyStates.Idle;
            return;
        }
    }
    private void Attack()
    {

    }
    private void Dead()
    {

    }
}
