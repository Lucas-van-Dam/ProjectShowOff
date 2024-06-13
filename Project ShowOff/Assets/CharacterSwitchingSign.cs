using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchingSign : MonoBehaviour
{

    [SerializeField]
    Characters switchTo;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.instance.ToggleCharacterSwitching((int)switchTo, true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.R))
            {
                other.gameObject.GetComponent<PlayerSwitching>().SwitchCharacter(switchTo);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.instance.ToggleCharacterSwitching((int)switchTo, false);
        }
    }
}
