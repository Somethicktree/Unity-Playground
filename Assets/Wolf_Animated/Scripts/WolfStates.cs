using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Rendering.InspectorCurveEditor;

public enum WolfState
{
    Idle,
    Moving,
    Chase
}

[RequireComponent(typeof(NavMeshAgent))]
public class WolfStates : MonoBehaviour
{
    [Header("Wander")]
    [SerializeField] private float wanderDistance = 50f; //How far the animal can moe in one go
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxWalkTime = 6;

    [Header("Idle")]
    [SerializeField] private float idleTime = 5f; //How long the animal takes a break for

    [Header("Chase")]
    [SerializeField] private float runSpeed = 8f;

    [Header("Attributes")]
    [SerializeField] private int health = 10;

    protected NavMeshAgent navMeshAgent;
    protected Animator anim;
    protected WolfState currentState = WolfState.Idle;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitializeWolf();
    }

    protected virtual void InitializeWolf()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkSpeed; //grabbing required componets of gameobject and setting up the variables within that

        currentState = WolfState.Idle;

        UpdateState();
    }

    protected virtual void UpdateState()
    {
        switch(currentState)
        {
            case WolfState.Idle:
                HandleIdleState();
                
                break;
            case WolfState.Moving:
                HandleMovingState();
                
                break;
            case WolfState.Chase:
                
                HandleChaseState();
                break;
        }
    }

    protected Vector3 GetRandomNavPos(Vector3 origin, float distance)
    {
        for(int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }
        }

        return origin;

    }

    protected virtual void CheckChaseConditions()
    {

    }

    protected virtual void HandleChaseState()
    {
        StopAllCoroutines();

    }

    protected virtual void HandleIdleState()
    {
        //anim.ResetTrigger("run");
        //anim.SetTrigger("idle");

        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToMove()
    {
        

        float waitTime = Random.Range(idleTime / 2, idleTime * 2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomNavPos(transform.position, wanderDistance);

        navMeshAgent.SetDestination(randomDestination); //Grabbin navmech agent from wolf and set its dest on the nav mesh

        SetState(WolfState.Moving);
    }

    protected virtual void HandleMovingState()
    {
        StartCoroutine(WaitToReachDest());
    }

    private IEnumerator WaitToReachDest()
    {
        float startTime = Time.time; //record current time in game to ensure that we can break
                                     //out of our movement if we havnet reached our destination within the time frame of our maxWalkTime
        while(navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && navMeshAgent.isActiveAndEnabled)
        {
            
            if (Time.time - startTime >= maxWalkTime)
            {
                navMeshAgent.ResetPath();
                Debug.Log("Idling");
                SetState(WolfState.Idle);
                yield break;
            }

            CheckChaseConditions(); 

            yield return null; //Everytime we get to this point check if we meet requirements if not look back and check again
        }

        //Destination has been reached
        //Do something when you reach dest here
        Debug.Log("Idling 1");
        SetState(WolfState.Idle);
    }

    protected void SetState(WolfState newState)
    {
        if(currentState == newState) 
            return;

        currentState = newState;
        OnStateChanged(newState);

    }

    protected virtual void OnStateChanged(WolfState newState)
    {

        anim?.CrossFadeInFixedTime(newState.ToString(), 0.5f);

        if (newState == WolfState.Moving)
            navMeshAgent.speed = walkSpeed;
            

        if (newState == WolfState.Chase)
            navMeshAgent.speed = runSpeed;


        UpdateState();
    }

    public virtual void RecieveDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
