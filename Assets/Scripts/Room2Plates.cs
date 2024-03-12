using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Plates : MonoBehaviour
{
    public LeverDoor door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dog"))
        {
            Debug.Log("Second Door Collide");
            door.OpenDoor();
        }
    }
}
