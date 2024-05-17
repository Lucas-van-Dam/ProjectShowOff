using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = Instantiate(spawnedObject);
        spawningGhost = Instantiate(spawningGhost);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
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
