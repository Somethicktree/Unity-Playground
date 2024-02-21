using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Exploder : MonoBehaviour
{
    public GameObject explosionPrefab;
   
    public void PlayExplosion()
    {
        //When Instantiating set the prefabs position and rotation to
        //this gameObjects position and rotation
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
