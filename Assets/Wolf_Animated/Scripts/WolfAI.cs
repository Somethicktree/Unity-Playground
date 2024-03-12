using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    public NavMeshAgent aI;
    public Transform player;
    public Animator aiAnim;

    

    Vector3 dest;

    void Update()
    {
        dest = player.position;
        aI.destination = dest;

        if(aI.remainingDistance <= aI.stoppingDistance)
        {
            aiAnim.ResetTrigger("run");
            aiAnim.SetTrigger("idle");
        }
        else
        {
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("run");
        }

        
       
    }

    
}
