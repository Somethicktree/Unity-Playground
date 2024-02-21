using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : WolfStates
{
    [Header("Pred Variables")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float maxChaseTime = 10f;
    [SerializeField] private int biteDamage = 3;
    [SerializeField] private float biteCoolDown = 1f;

    private Prey currentChaseTarget;

    
    protected override void CheckChaseConditions()
    {
        if (currentChaseTarget)
            return;

        Collider[] colliders = new Collider[10];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, colliders);

        for(int i = 0; i < numColliders; i++)
        {
            Prey prey = colliders[i].GetComponent<Prey>();

            if(prey != null)
            {
                StartChase(prey);
                return;
            }
        }

        currentChaseTarget = null;

    }

    private void StartChase(Prey prey)
    {
        currentChaseTarget = prey;
        SetState(WolfState.Chase);
       
    }

    protected override void HandleChaseState()
    {
        if(currentChaseTarget  != null)
        {
            currentChaseTarget.AlertPrey(this);
            StartCoroutine(ChasePrey());
        }
        else
        {
            SetState(WolfState.Idle);
        }
    }

    private IEnumerator ChasePrey()
    {
        float startTime = Time.time;

        while (currentChaseTarget != null && Vector3.Distance(transform.position, currentChaseTarget.transform.position) > navMeshAgent.stoppingDistance)
        {
            if(Time.time - startTime >= maxChaseTime || currentChaseTarget == null)
            {
                StopChase();
                yield break;
            }

            SetState(WolfState.Chase);
            navMeshAgent.SetDestination(currentChaseTarget.transform.position);

            yield return null;
        }

        if (currentChaseTarget)
            currentChaseTarget.RecieveDamage(biteDamage);
        //anim.Play("bite");

        yield return new WaitForSeconds(biteCoolDown);

        currentChaseTarget = null;
        HandleChaseState();

        CheckChaseConditions();

    }

    private void StopChase()
    {

        navMeshAgent.ResetPath();
        currentChaseTarget = null;
        SetState(WolfState.Idle);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
