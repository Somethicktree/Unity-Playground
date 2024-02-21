using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlameLight : MonoBehaviour
{
    private Light _light;
    private float multiplier = 1000f;
    public float fadeSpeed = 50;

    // Start is called before the first frame update
    private void Start()
    {
        _light = GetComponent<Light>();
    }
    
        

    // Update is called once per frame
    void Update()
    {
        _light.intensity = Random.Range(1.0f, 5.0f) * multiplier;
        multiplier -= fadeSpeed * Time.deltaTime;
        
        if(multiplier < 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
