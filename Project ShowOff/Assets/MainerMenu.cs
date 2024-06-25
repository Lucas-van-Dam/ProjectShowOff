using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainerMenu : MonoBehaviour
{

    [SerializeField] SceneSwitch sceneSwitch;

    // Start is called before the first frame update
    void Start()
    {
        if(sceneSwitch == null)
        {
            sceneSwitch = new SceneSwitch();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            sceneSwitch.scene_changer("Hub Area");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            sceneSwitch.QuitGame();
        }
    }
}
