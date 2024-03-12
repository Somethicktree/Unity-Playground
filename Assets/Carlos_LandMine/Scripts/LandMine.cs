using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandMine : MonoBehaviour
{
    public GameObject player;

    public GameObject dog;

    public UnityEvent mineExplosion;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Dog")
        {
            Debug.Log("MINE EXPLODE");
            mineExplosion?.Invoke();
            Destroy(this.gameObject);

            dog.gameObject.SetActive(false);
            //Destroy(player);
        }
    }
}
