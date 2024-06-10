using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{

    [SerializeField]
    Mesh[] tierMeshes;

    [SerializeField]
    public int tier;

    GameObject child;
    Animator animator;

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

        child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
    }

    public void Unlock()
    {
        animator.SetTrigger("OpenDoor");
    }
}
