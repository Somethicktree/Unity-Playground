using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator _Anim;

    private float doorDelay;

    [SerializeField] private float doorDuration;

    private bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        doorDelay += Time.deltaTime;

        if (doorDelay > doorDuration && doorIsOpen)
        {
            _Anim.SetTrigger("DoorTrigger");

            doorIsOpen = false;
            audio_manager.Instance.Play("DoorClose");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            _Anim.SetTrigger("DoorTrigger");

            audio_manager.Instance.Play("DoorCreek");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorIsOpen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorDelay = 0;
        }
    }

}
