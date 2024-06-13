using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSightScript : MonoBehaviour
{

    [SerializeField] InvisiblePlatformManager invisibleParent;
    [SerializeField] Material fogOverlay;

    float target;
    float transparancy;
    [SerializeField] float transitionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (invisibleParent == null)
        {
            //invisibleParent = InvisiblePlatformManager.Instance;
            //Debug.LogError("no reference to �nvisible platform manager!");
            invisibleParent = FindObjectOfType<InvisiblePlatformManager>();
        }
        if(fogOverlay == null)
        {
            Debug.LogError("no reference to fogoverlay for rex!");
        }

        fogOverlay.EnableKeyword("_Transparancy");
        fogOverlay.SetFloat("_Transparancy", 0f);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            target = invisibleParent.TogglePlatforms();
        }

        transparancy = Mathf.Lerp(transparancy, target, transitionSpeed);


        fogOverlay.SetFloat("_Transparancy", transparancy);
    }

    private void OnDisable()
    {
        invisibleParent.DisablePlatforms();
        fogOverlay.SetFloat("_Transparancy", 0);
    }

    private void OnDestroy()
    {
        invisibleParent.DisablePlatforms();
        fogOverlay.SetFloat("_Transparancy", 0);
    }
}
