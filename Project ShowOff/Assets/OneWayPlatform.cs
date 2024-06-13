using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    Transform player;
    Collider collider;

    [SerializeField]
    float offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y < transform.position.y + offset)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }
}
