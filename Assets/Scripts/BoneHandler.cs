using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneHandler : MonoBehaviour
{

    public float throwForce = 600;
    public GameObject tempParent;
    public bool isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(mouseRay, out hit);

        if (isHolding == true)
        {
            transform.position = tempParent.transform.position;
            transform.rotation = tempParent.transform.rotation;

            if (Input.GetMouseButtonDown(0))
            {
                //audio_manager.Instance.Play("Throw");

                //throw
                GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
                GetComponent<Rigidbody>().detectCollisions = true;
            }

        }
        else
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().useGravity = true;
        }

        if (Input.GetKeyDown("e") && !isHolding && hit.transform.position == transform.position)
        {
            audio_manager.Instance.Play("Bark");

            transform.SetParent(tempParent.transform);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            isHolding = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = false;
        }
        else if (Input.GetKeyDown("e") && isHolding)
        {
            //audio_manager.Instance.Play("Drop");

            isHolding = false;
            GetComponent<Rigidbody>().detectCollisions = true;
        }
    }
}
