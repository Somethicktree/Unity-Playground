using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Prey : WolfStates
{
    [Header("Prey Variables")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float escapeMaxDistance = 80f;

    private Predator currentPredator = null;

    public void AlertPrey(Predator predator)
    {
        SetState(WolfState.Chase);
        currentPredator = predator;
        StartCoroutine(RunFromPred());
    }

    private IEnumerator RunFromPred()
    {
        //Wait until the predator is within detection range
        while(currentPredator == null || Vector3.Distance(transform.position, currentPredator.transform.position) > detectionRange)
        {
            yield return null;
        }

        //Predator detected, so we should run away
        while(currentPredator != null && Vector3.Distance(transform.position, currentPredator.transform.position) <= detectionRange)
        {
            RunAwayFromPred();

            yield return null;
        }

        //Predator out of range, run to our final location and go back to idle
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            yield return null;
            
        }

        SetState(WolfState.Idle);
    }

    private void RunAwayFromPred()
    {
        if(navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                Vector3 runDirection = transform.position - currentPredator.transform.position;
                Vector3 escapeDest = transform.position + runDirection.normalized * (escapeMaxDistance * 2);
                navMeshAgent.SetDestination(GetRandomNavPos(escapeDest, escapeMaxDistance));
            }
        }
    }

    protected override void Die()
    {
        StopAllCoroutines();
        base.Die();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
