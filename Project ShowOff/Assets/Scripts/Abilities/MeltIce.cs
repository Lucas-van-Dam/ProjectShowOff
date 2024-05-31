using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeltIce : MonoBehaviour
{
    [FormerlySerializedAs("collider")] [SerializeField] Collider selfCollider;
    [SerializeField] bool fireOn;
    [FormerlySerializedAs("particleSystem")] [SerializeField] ParticleSystem FlameThrowerParticles;
    [FormerlySerializedAs("particleSystem")] [SerializeField] ParticleSystem FlameThrowerLightParticles;

    void Start()
    {
        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
        }
        if(FlameThrowerParticles == null)
        {
            FlameThrowerParticles = GetComponent<ParticleSystem>();
        }
        if(FlameThrowerLightParticles == null)
        {
            FlameThrowerLightParticles = FlameThrowerParticles.GetComponentInChildren<ParticleSystem>();
        }

        FlameThrowerParticles.Stop();
        FlameThrowerLightParticles.Stop();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            fireOn = !fireOn;
            if(fireOn)
            {
                FlameThrowerParticles.Play();
                FlameThrowerLightParticles.Play();
            }
            else
            {
                FlameThrowerParticles.Stop();
                FlameThrowerLightParticles.Stop();
            }
            
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (fireOn)
        {
            if (other.tag == "Meltable")
            {
                other.transform.localScale *= 0.99f;

                if(other.transform.localScale.y < 0.3f)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
