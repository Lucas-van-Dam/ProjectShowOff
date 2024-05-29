using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time));

        transform.rotation = Quaternion.Euler(Time.time * 40 * Mathf.PI, 0, Time.time * 20 * Mathf.PI);
    }
}
