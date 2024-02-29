using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public AudioSource audioSource;

    public Rigidbody controller;

    public float footstepThreshold;
    public float footstepRate;
    private float lastFootstepTime;

    void Update ()
    {
        // are we moving faster than the footstepThreshold?
        if(controller.velocity.magnitude > footstepThreshold)
        {
            // play a sound every footstepRate seconds
            if(Time.time - lastFootstepTime > footstepRate)
            {
                lastFootstepTime = Time.time;
                audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
            }
        }
    }
}