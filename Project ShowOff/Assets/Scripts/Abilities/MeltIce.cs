using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeltIce : MonoBehaviour
{
    [FormerlySerializedAs("collider")] [SerializeField] Collider selfCollider;
    [SerializeField] bool fireOn;
    [FormerlySerializedAs("particleSystem")] [SerializeField] ParticleSystem iceParticleSystem;

    void Start()
    {
        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
        }
        if(iceParticleSystem == null)
        {
            iceParticleSystem = GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            fireOn = !fireOn;
            if(fireOn)
            {
                iceParticleSystem.Play();
            }
            else
            {
                iceParticleSystem.Stop();
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
