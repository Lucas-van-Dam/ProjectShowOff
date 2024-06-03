using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchingSign : MonoBehaviour
{

    [SerializeField]
    Characters switchTo;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("111");
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("222");

            other.gameObject.GetComponent<PlayerSwitching>().SwitchCharacter(switchTo);
        }
    }
}
