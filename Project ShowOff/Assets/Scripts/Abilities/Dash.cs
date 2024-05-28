using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    
    [Header("References")] 
    private PlayerMovementAdvanced pm;
    private Transform orientation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
