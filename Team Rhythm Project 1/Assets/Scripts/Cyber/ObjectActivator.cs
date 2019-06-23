using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    // Used to check the tag of the object to be turned off.
    public string objectTag;

    // Reference to the spawn position
    public Transform spawnPosition;

    public float spawnOffset;

    public MeshGenerator meshGenerator;


    private void Start()
    {
          
    }

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
