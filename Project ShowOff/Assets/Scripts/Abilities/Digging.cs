using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    [Header("References")] 
    private PlayerMovementAdvanced pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovementAdvanced>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pm.crouchKey))
        {
            if (pm.crouching)
            {
                pm.StopDigging();
            }
            else
            {
                pm.StartDigging();
            }
        }
    }
}
