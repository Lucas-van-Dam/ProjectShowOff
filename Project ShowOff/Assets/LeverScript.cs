using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum triggerType
{
    Door,
    Platform
}

public class LeverScript : MonoBehaviour
{

    [SerializeField]
    GameObject triggeredObject;
    [SerializeField]
    triggerType triggerType;

    Animator animator;
    PlatformMover PlatformMover;

    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerType == triggerType.Door)
        {
            animator = triggeredObject.GetComponent<Animator>();
        }
        if (triggerType == triggerType.Platform)
        {
            PlatformMover = triggeredObject.GetComponent<PlatformMover>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if (Input.GetKeyDown(UIManager.instance.interactionKey))
            {
                if (triggerType == triggerType.Door)
                {
                    animator.SetTrigger("OpenDoor");
                }
                if (triggerType == triggerType.Platform)
                {
                    PlatformMover.TogglePlatform();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
