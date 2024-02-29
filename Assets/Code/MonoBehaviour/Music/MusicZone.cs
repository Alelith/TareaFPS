using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeTime;
    private float targetVolume;

    void Start ()
    {
        targetVolume = 0.0f;
        audioSource.volume = 0.0f;
    }

    void Update ()
    {
        // transition over time to the target volume
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (1.0f / fadeTime) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            targetVolume = 0.5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            targetVolume = 0.0f;
    }
}