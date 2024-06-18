using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

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

    [SerializeField]
    float yOffset;

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
                spawnPosition = hit.point + Vector3.up * yOffset;
            }
            else
            {

                RaycastHit hit2;

                if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * distance, Vector3.down, out hit2, distance, layerMask))
                {
                    spawnPosition = new Vector3(hit2.point.x, transform.position.y + yOffset, hit2.point.z);
                }
                    //spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            }

            if (Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Alpha3))
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
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                isSpawning = true;
            }
        }
    }

    private void OnDisable()
    {
        Destroy(spawnedObject);
        Destroy(spawningGhost);
    }
}
