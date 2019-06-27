using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovementBehaviour : MonoBehaviour
{

    // Similar to terrain movement behaviour

    /// Movement speed of this collectable instance.
    public float collectableMovementSpeed;

    // Stores a reference to the collectable manager.
    private GameObject collectableManager;

    void Start()
    {
        
    }


    //.
    void OnEnable()
    {
        // Find the collectable spawner with tag.
        collectableManager = GameObject.FindGameObjectWithTag("CollectableSpawner");

        // Access the terrain manager script on the spawner and set the speed.
        collectableMovementSpeed = collectableManager.gameObject.GetComponent<CollectableManager>().collectableSpeed;
    }

    // FixedUpdate is used for physics updates
    void FixedUpdate()
    {
        // Move this instance of terrain.
        transform.Translate(Vector3.back * Time.deltaTime * collectableMovementSpeed);
    }
}
