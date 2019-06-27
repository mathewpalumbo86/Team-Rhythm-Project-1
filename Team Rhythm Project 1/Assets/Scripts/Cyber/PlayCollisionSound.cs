using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to any object that will play a sound
public class PlayCollisionSound : MonoBehaviour
{
    [SerializeField]
    AudioSource[] soundEffects; // reference to the audio sources that are attached to the audio manager
    
    
    private void OnTriggerEnter(Collider other)
    {
        //ontrigger checks if the object is the player
        if (other.gameObject.tag == "Player")
        {
            int soundToPlay = RandomiseSoundEffect();
            soundEffects[soundToPlay].PlayOneShot(soundEffects[soundToPlay].clip); // plays the sound once on collsion
        }
    }

    //randomises the sound to play from the array
    int RandomiseSoundEffect()
    {
        int chooseSound = Random.Range(0, (soundEffects.Length - 1)); // minus one because using just the length of the array will cause an out of bounds error
        return chooseSound;
    }
}
