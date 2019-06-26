using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{       
    
    public string objectTag; // Used to check the tag of the object to be turned on.
    public Transform spawnPosition; // Reference to the spawn position.
    public float spawnOffset; // Offsets the objects position when spawned so they tile next to each other
    public MeshGenerator meshGenerator; // Stores the mesh generator script attached to the object entered the trigger
    
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("trigger entered");

        // Grabs the mesh generator of the object that collided and gets it's mesh z size. 
        // This is used to offset and tile the objects perfectly next to each other.
        meshGenerator = other.gameObject.GetComponent<MeshGenerator>();
        spawnOffset = meshGenerator.zSize;

        if (other.tag == objectTag)
        {
            // Debug.Log("tag is true");
            // other.gameObject.SetActive(false);
            GameObject objectToActivate = ObjectPooler.SharedInstance.GetPooledObject("Terrain");
            if (objectToActivate != null)
            {
                // Sets the position of each object, allowing for the position offset
                objectToActivate.transform.position = new Vector3(spawnPosition.transform.position.x, spawnPosition.transform.position.y, (spawnPosition.transform.position.z + spawnOffset));
                objectToActivate.transform.rotation = spawnPosition.transform.rotation;
                objectToActivate.SetActive(true);
                // Debug.Log("spawn offset = " + spawnOffset);
            }
        }
    }
}
