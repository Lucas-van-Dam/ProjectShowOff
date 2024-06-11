using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIPromt : MonoBehaviour
{
    public Sprite promtImage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        UIManager.instance.promtImage.sprite = promtImage;
        UIManager.instance.promtImage.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        UIManager.instance.promtImage.gameObject.SetActive(false);
    }
}
