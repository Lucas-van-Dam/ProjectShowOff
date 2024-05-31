using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashJumpForceUp;
    [SerializeField] private float dashJumpForceForwards;

    [Header("Input")] 
    [SerializeField] private KeyCode DashKey;
    
    [Header("References")] 
    private PlayerMovementAdvanced pm;
    private Transform orientation;
    private Rigidbody rb;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovementAdvanced>();
        rb = GetComponentInParent<Rigidbody>();
        orientation = pm.orientation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(DashKey) && !pm.dashing)
        {
            StartCoroutine(DoDash());
        }

        if (Input.GetKeyDown(pm.jumpKey))
        {
            if (!pm.dashing)
                return;
            
            StopCoroutine(DoDash());
            rb.AddForce(transform.forward * dashJumpForceForwards + transform.up * dashJumpForceUp);
            StartCoroutine(StopDash());
            
        }
    }

    IEnumerator DoDash()
    {
        pm.dashing = true;
        rb.velocity = Vector3.zero;
        rb.drag = 0;
        rb.AddForce(transform.forward * dashForce);
        yield return new WaitForSeconds(dashDuration);
        StartCoroutine(StopDash());
    }

    IEnumerator StopDash()
    {
        yield return new WaitUntil(() => pm.grounded);
        Debug.Log("Stopped Dashing");
        pm.dashing = false;
    }
}
