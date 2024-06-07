using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fragment")
        {
            Destroy(other.gameObject);
            UIManager.instance.FragmentCollectedEvent();
        }

        if(other.tag == "Locked")
        {
            if(other.GetComponent<LockScript>() != null)
            {
                if(UIManager.instance.keyTier >= other.GetComponent<LockScript>().tier)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
