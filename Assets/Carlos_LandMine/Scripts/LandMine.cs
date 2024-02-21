using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandMine : MonoBehaviour
{
    public GameObject player;

    public UnityEvent mineExplosion;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("MINE EXPLODE");
            mineExplosion?.Invoke();
            Destroy(this.gameObject);
            //Destroy(player);
        }
    }
}
