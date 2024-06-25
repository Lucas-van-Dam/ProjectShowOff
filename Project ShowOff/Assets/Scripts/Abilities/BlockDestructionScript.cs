using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockDestructionScript : MonoBehaviour
{
    [FormerlySerializedAs("collider")] [SerializeField]
    Collider selfCollider;

    [Header("References")] private PlayerMovementAdvanced pm;

    void Start()
    {
        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
        }

        pm = GetComponentInParent<PlayerMovementAdvanced>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button3) && !pm.crouching)
        {
            pm.animator.SetTrigger("Chomp");
            SoundManager.instance.PlaySound("bite");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Joystick1Button3))
        {
            if (other.tag == "Destructible")
            {
                pm.animator.SetTrigger("Chomp");
                other.GetComponent<DestructibleBlock>().DestroyThis();
                SoundManager.instance.PlaySound("rockcrumble");
            }
        }

        if (other.tag == "Destructible")
        {
            other.GetComponent<DestructibleBlock>().StartAnimating();
        }
    }
}