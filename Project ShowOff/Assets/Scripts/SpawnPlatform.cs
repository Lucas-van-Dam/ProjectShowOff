using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnPlatform : MonoBehaviour
{

    [SerializeField]
    GameObject spawnedObject;
    [SerializeField]
    GameObject spawningGhost;
    Vector3 spawnPosition;

    [SerializeField]
    float distance;

    bool isSpawning;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = Instantiate(spawnedObject);
        spawningGhost = Instantiate(spawningGhost);

        layerMask = (1 << 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance, layerMask))
            {
                spawnPosition = hit.point;
            }
            else
            {
                spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                spawnedObject.transform.position = spawnPosition;
                spawnedObject.SetActive(true);
                spawningGhost.SetActive(false);
                isSpawning = false;
            }
            else
            {
                spawningGhost.transform.position = spawnPosition;
                spawningGhost.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isSpawning = true;
            }
        }
    }
}
