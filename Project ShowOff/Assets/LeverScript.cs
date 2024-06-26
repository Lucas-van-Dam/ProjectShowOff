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

    [SerializeField]
    Transform animating;
    float target;
    float currentAngle;

    [SerializeField]
    float leverRotSpeed;

    // Start is called before the first frame update
    void Start()
    {

        if(animating == null)
        {
            animating = transform.GetChild(0);
        }

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

                if(animating != null)
                {
                    if(target == 0)
                    {
                        target = 140;
                        currentAngle = 1;
                    }
                    else
                    {
                        target = 0;
                        currentAngle = 139;
                    }
                }

                SoundManager.instance.PlaySound("lever");

                if(onlyOnce)
                {
                    isOn = false;
                }
            }
        }

        if(currentAngle > 0 && currentAngle < 140)
        {
            if (target == 0)
            {
                currentAngle -= leverRotSpeed;
            }
            else
            {
                currentAngle += leverRotSpeed;
            }
        }

        animating.localRotation = Quaternion.Euler(new Vector3(currentAngle, 0, 0));
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
