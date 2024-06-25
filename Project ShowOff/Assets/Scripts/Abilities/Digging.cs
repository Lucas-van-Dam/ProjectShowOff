using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    [Header("References")] 
    private PlayerMovementAdvanced pm;

    private Collider col;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovementAdvanced>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pm.crouchKey))
        {
            if (pm.crouching)
            {
                pm.StopDigging();

                SoundManager.instance.StopLoop("digging");
            }
            else
            {
                pm.StartDigging();

                SoundManager.instance.PlayLoop("digging");
            }
        }
    }

    private void OnDestroy()
    {
        pm.StopDigging();

        SoundManager.instance.StopLoop("digging");
    }
}
