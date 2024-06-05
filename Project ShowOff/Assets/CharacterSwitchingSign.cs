using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchingSign : MonoBehaviour
{

    [SerializeField]
    Characters switchTo;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                other.gameObject.GetComponent<PlayerSwitching>().SwitchCharacter(switchTo);
            }

        }
    }
}
