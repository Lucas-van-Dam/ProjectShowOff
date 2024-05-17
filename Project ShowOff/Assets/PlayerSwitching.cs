using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using UnityEngine;

public enum Characters
{
    VF = 0,
    Cutizylo = 1
}

public class PlayerSwitching : MonoBehaviour
{
    
    public Characters currentCharacter;
    [SerializeField] private GameObject graphicsHolder;
    [SerializeField] private PlayerMovementAdvanced movement;

    [Header("VF")] 
    [SerializeField] private CharacterData VFData;
    
    [Header("Cutizylo")] 
    [SerializeField] private CharacterData CutizyloData;
    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = Characters.VF;
        SetVariables(VFData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        if ((int)currentCharacter < 1)
        {
            currentCharacter++;
        }
        else
        {
            currentCharacter = 0;
        }
        switch (currentCharacter)
        {
            case Characters.VF:
                SetVariables(VFData);
                break;
            
            case Characters.Cutizylo:
                SetVariables(CutizyloData);
                break;
            
            default:
                Debug.LogError("SOMETHING WENT WRONG WITH CHARACTER SWITCHING");
                break;
        }
    }

    private void SetVariables(CharacterData data)
    {
        movement.walkSpeed = data.walkSpeed;
        movement.sprintSpeed = data.sprintSpeed;
        movement.jumpForce = data.jumpPower;
        
        //SET MODEL
        for (int i = graphicsHolder.transform.childCount - 1; i > -1; i--)
        {
            Destroy(graphicsHolder.transform.GetChild(i).gameObject);
        }

        Instantiate(data.CharacterModel, transform.position + Vector3.down, graphicsHolder.transform.rotation, graphicsHolder.transform);
    }
}
