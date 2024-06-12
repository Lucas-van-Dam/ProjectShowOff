using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        if (Input.GetKeyDown(pm.jumpKey))
        {
            if (!pm.dashing)
                return;
            

            StopCoroutine(DoDash());
            rb.AddForce(-transform.right * dashJumpForceForwards + transform.up * dashJumpForceUp);
            StartCoroutine(StopDash());
            pm.animator.SetTrigger("DashJump");
            
        }
        
        if (Input.GetKeyDown(DashKey) && !pm.dashing)
        {
            StopAllCoroutines();
            StartCoroutine(DoDash());
        }


    }

    IEnumerator DoDash()
    {
        pm.animator.SetTrigger("Dash");
        pm.dashing = true;
        rb.velocity = Vector3.zero;
        rb.drag = 0;
        rb.AddForce(-transform.right * dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(dashDuration);
        StartCoroutine(StopDash());
    }

    IEnumerator StopDash()
    {

        int counter = 0;
        while (counter <= 50 && !pm.grounded)
        {
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        Debug.Log("Stopped Dashing");
        pm.dashing = false;
    }
}
