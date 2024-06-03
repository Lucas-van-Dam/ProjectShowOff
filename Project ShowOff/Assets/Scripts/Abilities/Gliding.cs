using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    [Header("References")] 
    private PlayerMovementAdvanced pm;
    private Transform orientation;
    private Rigidbody rb;

    [Header("Input")] 
    [SerializeField] private KeyCode glideKey = KeyCode.KeypadPeriod;

    [Header("Stats")] 
    [SerializeField] private float glideHopUpForce;
    [SerializeField] private float glideHopForwardForce;
    

    private bool hop = true;

    void Start()
    {
        pm = GetComponentInParent<PlayerMovementAdvanced>();
        rb = GetComponentInParent<Rigidbody>();
        orientation = pm.orientation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(glideKey))
        {
            if (hop)
            {
                pm.gliding = true;
                hop = false;
                rb.drag = 0;
                rb.AddForce(glideHopUpForce * transform.up + glideHopForwardForce * transform.forward, ForceMode.Impulse);
                rb.mass = 0.1f;
            }
            else
            {
                rb.drag = 0;
                pm.gliding = true;
                rb.mass = 0.1f;
            }
        }
        else if (pm.grounded && !hop)
        {
            hop = true;
            rb.mass = 1;
            pm.gliding = false;
        }
        else
        {
            pm.gliding = false;
            rb.mass = 1;
        }

    }
}
