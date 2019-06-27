using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableOnCollision : MonoBehaviour
{
    // Collectable audio effects script
    public CollectableAudioEffects collectableAudioEffects;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // get the collectable audio effects script
        collectableAudioEffects = GetComponent<CollectableAudioEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // if the player hits this object play a collectable sound effect and set it to inactive
            collectableAudioEffects.PlayCollectableEffect();
            other.gameObject.SetActive(false);
        }
    }
}
