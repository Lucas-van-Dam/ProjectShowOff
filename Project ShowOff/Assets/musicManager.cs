using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstep1;
    public AudioClip footstep2;
    private bool isPlayingFootstep1 = true;
    public Rigidbody characterRigidbody;
    public float footstepDelay = 0.5f; // Time between footsteps
    private float nextFootstepTime = 0f;

    void Update()
    {
        // Check if the character is grounded and moving
        if (IsGrounded() && characterRigidbody.velocity.magnitude > 0.1f)
        {
            // Check if it's time to play the next footstep sound
            if (Time.time >= nextFootstepTime)
            {
                PlayFootstep();
                nextFootstepTime = Time.time + footstepDelay;
            }
        }
    }
    //asf
    void PlayFootstep()
    {
        audioSource.clip = isPlayingFootstep1 ? footstep1 : footstep2;
        audioSource.Play();
        isPlayingFootstep1 = !isPlayingFootstep1;
    }

    bool IsGrounded()
    {
        // Perform a raycast downwards to check if the character is grounded
        return Physics.Raycast(characterRigidbody.transform.position, Vector3.down, 1.1f);
    }
}