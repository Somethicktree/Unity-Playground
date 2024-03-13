using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    public LeverDoor door;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dog"))
        {
            //Debug.Log("Collided");
            door.OpenDoor();
        }
    }
}
