using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSightScript : MonoBehaviour
{

    [SerializeField] InvisiblePlatformManager invisibleParent;

    // Start is called before the first frame update
    void Start()
    {
        if (invisibleParent == null)
        {
            invisibleParent = InvisiblePlatformManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            invisibleParent.TogglePlatforms();
        }
    }

    private void OnDisable()
    {
        invisibleParent.DisablePlatforms();
    }

    private void OnDestroy()
    {
        invisibleParent.DisablePlatforms();
    }
}
