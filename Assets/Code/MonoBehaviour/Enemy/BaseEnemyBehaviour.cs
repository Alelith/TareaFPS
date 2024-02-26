using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float playerProximity;
    [SerializeField]
    protected float attackProximity;

    protected StateMachine brain;
    protected NavMeshAgent agent;
    protected PlayerController player;

    protected bool playerIsNear;
    protected bool withinAttackRange;

    protected float changeMind;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        brain = GetComponent<StateMachine>();

        playerIsNear = false;
        withinAttackRange = false;

        brain.PushState(Idle, OnIdleEnter, OnIdleExit);
    }

    protected virtual void Update()
    {
        playerIsNear = Vector3.Distance(transform.position, player.transform.position) < playerProximity;

        withinAttackRange = Vector3.Distance(transform.position, player.transform.position) < attackProximity;
    }

    #region Idle Functions
    protected virtual void OnIdleEnter()
    {
        
    }

    protected virtual void Idle()
    {
        changeMind -= Time.deltaTime;
        if (playerIsNear)
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        else if (changeMind <= 0)
        {
            brain.PushState(Wander, OnWanderEnter, OnWanderExit);
            changeMind = Random.Range(2, 5);
        }
    }

    protected virtual void OnIdleExit()
    {
        
    }
    #endregion

    #region Chase Functions
    protected virtual void OnChaseEnter()
    {

    }
    
    protected virtual void Chase()
    {
        //<agent.SetDestination(player.transform.position);
        if (!playerIsNear)
            brain.PopState();
    }

    protected virtual void OnChaseExit()
    {

    }

    #endregion

    #region Wander Functions
    protected virtual void OnWanderEnter()
    {
        Vector3 wanderDirection = (Random.insideUnitSphere * 4) + transform.position;

        NavMesh.SamplePosition(wanderDirection, out NavMeshHit navMeshHit, 3f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;

        agent.SetDestination(destination);
    }

    protected virtual void Wander()
    {
        if (agent.remainingDistance <= .25f)
        {
            agent.ResetPath();
            brain.PopState();
        }
        if (playerIsNear)
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
    }

    protected virtual void OnWanderExit()
    {

    }
    #endregion
}
