using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to any object that will play a sound
[RequireComponent(typeof(Collider))]
public class PlayCollisionSound : MonoBehaviour
{
    [SerializeField]
    AudioSource soundEffect; // reference to the audio source that's attached to the audio manager

    private void OnCollisionEnter(Collision collision)
    {
        //checks if the collision is coming from the player, might want to swap this to an OnTriggerEnter.
        if (collision.collider.tag == "player")
        {
            soundEffect.PlayOneShot(soundEffect.clip); // plays the sound once on collsion
            
        }
    }
}
