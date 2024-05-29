using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    //script to play sound when colliding with a trigger with tag JumpSound random one from multiple audio clips
    public List<AudioClip> jumpClips; // List of audio clips
    public AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpSound"))
        {
            // Play a random jump sound from the list
            audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Count)]);
        }
    }
    
    
}
