using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BlockDestructionScript : MonoBehaviour
{

    [SerializeField] Collider collider;

    void Start()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider>();
        }
    }

    void Update()
    {
        

        //RaycastHit hit;

        //if (collider.)

        //if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        //{
        //    if(hit.collider.tag == "Destructible")
        //    {
        //        hit.collider.gameObject.SetActive(false);
        //    }
        //}
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (other.tag == "Destructible")
            {
                other.GetComponent<DestructibleBlock>().DestroyThis();
            }
        }

        if (other.tag == "Destructible")
        {
            other.GetComponent<DestructibleBlock>().StartAnimating();
        }
    }
}
