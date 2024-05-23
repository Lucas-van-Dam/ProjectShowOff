using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructionScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if(hit.collider.tag == "Destructible")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
