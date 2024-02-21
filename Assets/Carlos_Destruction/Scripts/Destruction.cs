using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    //private List<Rigidbody> rb;

    public GameObject destroyedVersion;

    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Instantiate(destroyedVersion, transform.position, transform.rotation);

            Destroy(gameObject);

            
            //Temp.GetComponent<Rigidbody>().AddForce(collision.gameObject.transform.forward * 5);

        }
    }
}
