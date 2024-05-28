using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public GameObject CharacterModel;
    public float jumpPower;
    public float walkSpeed;
    public float sprintSpeed;
}