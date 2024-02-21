using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class BombFuse : MonoBehaviour
{
    public VisualEffect fuse;

    public VisualEffect rope;

    private float time = 0;

    bool timeBool = false;

    public UnityEvent explosion;

    public float delay = 4;

    Coroutine explosionDelay;
   

    // Update is called once per frame
    void Update()
    {
        if (timeBool)
        {
            time += Time.deltaTime;
            fuse.SetFloat("Time", time);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            timeBool = true;
            time = 0;
            fuse.SendEvent("Fuse");
            
            explosionDelay = StartCoroutine(BombDelay(delay));
            Destroy(rope.gameObject);

        }

    }

    IEnumerator BombDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        explosion.Invoke();

    }
}
