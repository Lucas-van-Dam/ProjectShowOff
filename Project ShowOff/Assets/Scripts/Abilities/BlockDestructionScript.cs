using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockDestructionScript : MonoBehaviour
{

    [FormerlySerializedAs("collider")] [SerializeField] Collider selfCollider;

    void Start()
    {
        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
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
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button3))
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
