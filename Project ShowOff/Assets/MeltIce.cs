using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltIce : MonoBehaviour
{
    [SerializeField] Collider collider;
    [SerializeField] bool fireOn;
    [SerializeField] ParticleSystem particleSystem;

    void Start()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider>();
        }
        if(particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            fireOn = !fireOn;
            if(fireOn)
            {
                particleSystem.Play();
            }
            else
            {
                particleSystem.Stop();
            }
            
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (other.tag == "Meltable")
            {
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Meltable")
        {
            //other.GetComponent<DestructibleBlock>().StartAnimating();
        }
    }
}
