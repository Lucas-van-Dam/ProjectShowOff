using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFAbilityController : MonoBehaviour
{

    [SerializeField]
    private WaterCastScript waterCastScript;
    [SerializeField]
    private MeltIce meltIceScript;
    
    PlayerMovementAdvanced characterController;

    bool castingWater;
    bool castingFire;

    // Start is called before the first frame update
    void Start()
    {
        if(waterCastScript == null)
        {
            waterCastScript = GetComponentInChildren<WaterCastScript>();
        }
        if(meltIceScript == null)
        {
            meltIceScript = GetComponentInChildren<MeltIce>();
        }

        characterController = GetComponentInParent<PlayerMovementAdvanced>();
    }

    // Update is called once per frame
    void Update()
    {
        //water
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!castingWater)
            {
                waterCastScript.startCasting();
                castingWater = true;

                if(castingFire)
                {
                    meltIceScript.stopCasting();
                    castingFire = false;
                }
            }
            else
            {
                waterCastScript.stopCasting();
                castingWater = false;
            }
        }

        //fire
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!castingFire)
            {
                meltIceScript.startCasting();
                castingFire = true;

                if (castingWater)
                {
                    waterCastScript.stopCasting();
                    castingWater = false;
                }
            }
            else
            {
                meltIceScript.stopCasting();
                castingFire = false;
            }
        }

        if(castingWater || castingFire)
        {
            characterController.freeze = true;
        }
        else
        {
            characterController.freeze = false;
        }
    }
}
