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
    GameObject[] triggeredObject;
    [SerializeField]
    triggerType triggerType;

    [SerializeField]
    bool onlyOnce;

    bool isOn = true;

    Animator[] animator;
    PlatformMover[] PlatformMover;

    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {

        if (triggeredObject.Length == 0)
        {
            isOn = false;
        }
        else
        {
            if (triggerType == triggerType.Door)
            {

                animator = new Animator[triggeredObject.Length];

                for (int i = 0; i < triggeredObject.Length; i++)
                {
                    animator[i] = triggeredObject[i].GetComponent<Animator>();
                }
            }
            if (triggerType == triggerType.Platform)
            {

                PlatformMover = new PlatformMover[triggeredObject.Length];

                for (int i = 0; i < triggeredObject.Length; i++)
                {
                    PlatformMover[i] = triggeredObject[i].GetComponent<PlatformMover>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && isOn)
        {
            if (Input.GetKeyDown(UIManager.instance.interactionKey))
            {
                if (triggerType == triggerType.Door)
                {
                    for (int i = 0; i < triggeredObject.Length; i++)
                    {
                        animator[i].SetTrigger("OpenDoor");
                    }
                }
                if (triggerType == triggerType.Platform)
                {
                    for (int i = 0; i < triggeredObject.Length; i++)
                    {
                        PlatformMover[i].TogglePlatform();
                    }
                }

                if(onlyOnce)
                {
                    isOn = false;
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
