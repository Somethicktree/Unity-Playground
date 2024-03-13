using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{
    Animator _Anim;

    // Start is called before the first frame update
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        //Debug.Log("Opening Door");
        audio_manager.Instance.Play("DoorCreek");
        _Anim.SetTrigger("DoorTrigger");
    }

}
