using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeltIce : MonoBehaviour
{
    [FormerlySerializedAs("collider")] [SerializeField]
    Collider selfCollider;

    public bool fireOn;

    [FormerlySerializedAs("particleSystem")] [SerializeField]
    ParticleSystem FlameThrowerParticles;

    [FormerlySerializedAs("particleSystem")] [SerializeField]
    ParticleSystem FlameThrowerLightParticles;

    private PlayerMovementAdvanced pm;

    [SerializeField] private WaterCastScript waterCast;

    void Start()
    {
        pm = GetComponentInParent<PlayerMovementAdvanced>();
        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
        }

        if (FlameThrowerParticles == null)
        {
            FlameThrowerParticles = GetComponent<ParticleSystem>();
        }

        if (FlameThrowerLightParticles == null)
        {
            FlameThrowerLightParticles = FlameThrowerParticles.GetComponentInChildren<ParticleSystem>();
        }

        FlameThrowerParticles.Stop();
        FlameThrowerLightParticles.Stop();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        //{
        //    if (!fireOn && !waterCast.isOn)
        //    {
        //        fireOn = true;
        //        pm.animator.SetBool("Fire", true);
        //        FlameThrowerParticles.Play();
        //        FlameThrowerLightParticles.Play();
        //    }
        //    else if (fireOn)
        //    {
        //        fireOn = false;
        //        pm.animator.SetBool("Fire", false);
        //        FlameThrowerParticles.Stop();
        //        FlameThrowerLightParticles.Stop();
        //    }
        //}
    }

    public void startCasting()
    {
        fireOn = true;
        pm.animator.SetBool("Fire", true);
        FlameThrowerParticles.Play();
        FlameThrowerLightParticles.Play();
    }

    public void stopCasting()
    {
        fireOn = false;
        pm.animator.SetBool("Fire", false);
        FlameThrowerParticles.Stop();
        FlameThrowerLightParticles.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (fireOn)
        {
            if (other.tag == "Meltable")
            {
                other.transform.localScale *= 0.99f;

                if (other.transform.localScale.y < 0.3f)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}