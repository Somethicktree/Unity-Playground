using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    public NavMeshAgent aI;
    public Transform player;
    public Animator aiAnim;

    float delay = 4.0f;

    Vector3 dest;

    void Update()
    {
        dest = player.position;
        aI.destination = dest;

        if(aI.remainingDistance <= aI.stoppingDistance)
        {
            aiAnim.ResetTrigger("run");
            aiAnim.SetTrigger("bite");
        }
        else
        {
            aiAnim.ResetTrigger("bite");
            aiAnim.SetTrigger("run");
        }

        StartCoroutine(WaitFunction(delay));
       
    }

    //float delay = 4.0f;

    IEnumerator WaitFunction(float delayTime)

    {

        delayTime += 2.0f;

        yield return null;

        Debug.Log("I am a debug log!");

    }
}
