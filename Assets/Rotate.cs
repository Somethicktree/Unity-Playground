using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float spd = 0.5f;
    [SerializeField] Vector3 axis;
    private void Update()
    {
        transform.Rotate(axis * spd * 10 * Time.deltaTime);
    }
}
