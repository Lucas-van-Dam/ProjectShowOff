using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{

    [SerializeField]
    Mesh[] tierMeshes;

    [SerializeField]
    public int tier;

    [SerializeField]
    GameObject lockedObject;
    Animator animator;
    Rigidbody rb;

    bool locked = true;

    // Start is called before the first frame update
    void Start()
    {
        if(tier >= 0 && tier < tierMeshes.Length)
        {
            GetComponent<MeshFilter>().mesh = tierMeshes[tier];
        }
        else
        {
            Debug.LogError("lock tier not in valid range!");
        }

        //lockedObject = transform.GetChild(0).gameObject;
        if(lockedObject != null)
        {
            animator = lockedObject.GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            if(rb == null)
            {
                Debug.LogError("no rigidbody attached to " + name + "!");
            }
        }
        else
        {
            Debug.LogError("no locked object defined to lock " + name + "!");
        }

    }

    public void Unlock()
    {
        animator.SetTrigger("OpenDoor");

        rb.constraints = RigidbodyConstraints.None;

        locked = false;
    }

    private void Update()
    {
        if (!locked)
        {

            float scale = 0.999f / (1.001f / transform.localScale.x);

            if(scale < 0.1f)
            {
                Destroy(gameObject);
            }

            transform.localScale = new Vector3 (scale, scale, scale);
        }
    }
}
