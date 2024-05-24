using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSightScript : MonoBehaviour
{

    [SerializeField] InvisiblePlatformManager invisibleParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            invisibleParent.TogglePlatforms();
        }
    }
}
