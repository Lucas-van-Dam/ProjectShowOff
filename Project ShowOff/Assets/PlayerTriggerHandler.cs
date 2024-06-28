using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerTriggerHandler : MonoBehaviour
{

    bool respawnNextPhysUpdate;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.respawnPoint = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fragment")
        {
            Destroy(other.gameObject);
            UIManager.instance.FragmentCollectedEvent();
            switch(Random.Range(0, 3))
            {
                case 0:
                    SoundManager.instance.PlaySound("pickup1");
                    break;
                case 1:
                    SoundManager.instance.PlaySound("pickup2");
                    break;
                case 2:
                    SoundManager.instance.PlaySound("pickup3");
                    break;
            }
            
        }

        if (other.tag == "Totem")
        {
            Destroy(other.gameObject);
            UIManager.instance.TotemCollected();
            SoundManager.instance.PlaySound("totem");
        }

        if(other.tag == "Locked")
        {

            LockScript lockScript = other.GetComponent<LockScript>();

            if (lockScript != null)
            {
                if(UIManager.instance.keyTier >= lockScript.tier)
                {
                    //Destroy(other.gameObject);
                    lockScript.Unlock();
                    SoundManager.instance.PlaySound("unlock");
                }
            }
        }

        if(other.tag == "RespawnPoint")
        {
            UIManager.instance.respawnPoint = other.transform.position + Vector3.up * 2;
        }
        if (other.tag == "EndGame")
        {

            //END THE GAME
            UIManager.instance.OpenEndGameCanvas();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ResetPlayer")
        {
            transform.position = UIManager.instance.respawnPoint;
        }
        if(collision.collider.tag == "Water")
        {
            SoundManager.instance.PlaySound("splash");
            transform.position = UIManager.instance.respawnPoint;
        }
    }

    private void FixedUpdate()
    {
        if (respawnNextPhysUpdate)
        {
            transform.position = UIManager.instance.respawnPoint;
            respawnNextPhysUpdate = false;
        }
    }

    public void ResetPosition()
    {
        respawnNextPhysUpdate = true;
    }
}
