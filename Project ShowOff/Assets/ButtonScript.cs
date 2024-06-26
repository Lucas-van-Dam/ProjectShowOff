using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    GameObject triggeredObject;
    [SerializeField]
    triggerType triggerType;

    Animator animator;
    PlatformMover PlatformMover;

    bool playerInRange;

    [SerializeField]
    float timer;

    bool timing;
    float nextTime;

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
        if(timer > 0 && timing)
        {
            if(nextTime < Time.time)
            {
                timing = false;
                animator.SetTrigger("CloseDoor");
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAaa");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SoundManager.instance.PlaySound("buttonclick");

            if (triggerType == triggerType.Door)
            {
                animator.SetTrigger("OpenDoor");
                SoundManager.instance.PlaySound("opendoor");
                nextTime = Time.time + timer;

                Debug.Log(nextTime);
                Debug.Log(Time.time);

                timing = true;
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerInRange = false;
    //    }
    //}
}
