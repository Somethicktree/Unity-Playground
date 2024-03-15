using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandMine : MonoBehaviour
{
    public GameObject player;

    public GameObject dog;

    public UnityEvent mineExplosion;

    public GameManager manager;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Dog")
        {
            Debug.Log("MINE EXPLODE");
            mineExplosion?.Invoke();
           

            dog.gameObject.SetActive(false);
            //Destroy(player);

            manager.Lost();

            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("MINE EXPLODE");
            mineExplosion?.Invoke();
            

            manager.player.enabled = false;
            manager.playerLook.enabled = false;
            manager.crosshairs.SetActive(false);
            manager.Lost();

            Destroy(this.gameObject);
            //Destroy(player);
        }
    }
}
