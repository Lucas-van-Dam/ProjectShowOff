using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMover : MonoBehaviour
{

    //general variables
    [SerializeField]
    Transform start;
    [SerializeField]
    Transform end;
    [SerializeField]
    bool triggered;

    [SerializeField]
    float lerpAmount;
    [SerializeField]
    float speed;

    //travel info
    Vector3 startPos;
    Vector3 endPos;
    float distance;

    //float based position
    float target;
    float current;

    bool dir;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        if(start != null)
        {
            startPos = start.position;
            endPos = end.position;
        }
        else if(transform.childCount > 0)
        {
            transform.GetChild(0).position = startPos;
            transform.GetChild(1).position = endPos;
        }
        else 
        {
            Debug.LogError("No target positions set or found!");
        }

        distance = Vector3.Distance(startPos, endPos);
        target = distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered)
        {
            if (dir)
            {
                current += speed * Time.deltaTime;
                current = Mathf.Lerp(current, target, lerpAmount / (1 / (current + 0.001f)));

                if (current > target)
                {
                    dir = !dir;
                    target = 0;
                }
            }
            else
            {
                current -= speed * Time.deltaTime;
                current = Mathf.Lerp(current, target, lerpAmount / (1 / (-(current - distance) + 0.001f)));

                if (current < target)
                {
                    dir = !dir;
                    target = distance;
                }
            }
        }
        else if (active)
        {
            if (!dir)
            {
                current += speed * Time.deltaTime;
                current = Mathf.Lerp(current, target, lerpAmount / (1 / (current + 0.001f)));

                if (current > target)
                {
                    current = target;
                    active = false;
                }
            }
            else
            {
                current -= speed * Time.deltaTime;
                current = Mathf.Lerp(current, target, lerpAmount / (1 / (-(current - distance) + 0.001f)));

                if (current < target)
                {
                    current = target;
                    active = false;
                }
            }
        }

        transform.position = startPos + ((endPos - startPos) * (current / distance));

    }

    public void TogglePlatform()
    {
        dir = !dir;
        active = true;

        if (dir) target = 0;
        else target = distance;
    }
}
