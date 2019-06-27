using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{       
    
    public string objectTag; // Used to check the tag of the object to be turned on.
    public Transform terrainSpawnPosition; // Reference to the terrain spawn position.
    public float spawnOffset; // Offsets the objects position when spawned so they tile next to each other
    public MeshGenerator meshGenerator; // Stores the mesh generator script attached to the object entered the trigger

    private float objectDelay; // For setting a delay between object placements
    public Transform objectSpawnPosition; // Reference to the object spawn position.
    public float collectableRandMin;
    public float collectableRandMax; 

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("trigger entered");        

        if (other.tag == "Terrain")
        {
            // Grabs the mesh generator of the object that collided and gets it's mesh z size. 
            // This is used to offset and tile the objects perfectly next to each other.
            meshGenerator = other.gameObject.GetComponent<MeshGenerator>();
            spawnOffset = meshGenerator.zSize;


            // Debug.Log("tag is true");
           
            GameObject objectToActivate = ObjectPooler.SharedInstance.GetPooledObject("Terrain");
            if (objectToActivate != null)
            {
                // Sets the position of each object, allowing for the position offset
                objectToActivate.transform.position = new Vector3(terrainSpawnPosition.transform.position.x, terrainSpawnPosition.transform.position.y, (terrainSpawnPosition.transform.position.z + spawnOffset));
                objectToActivate.transform.rotation = terrainSpawnPosition.transform.rotation;
                objectToActivate.SetActive(true);
                // Debug.Log("spawn offset = " + spawnOffset);
            }
        }
        else if (other.tag == "Collectable")
        {
            
            objectDelay = Random.Range(collectableRandMin, collectableRandMax);
            DelayObjectPlacement();

            Debug.Log("is a collectable");

            // Debug.Log("tag is true");
            
            GameObject objectToActivate = ObjectPooler.SharedInstance.GetPooledObject("Collectable");
            if (objectToActivate != null)
            {
                // Sets the position of each object, allowing for the position offset
                objectToActivate.transform.position = new Vector3(objectSpawnPosition.transform.position.x, objectSpawnPosition.transform.position.y, (objectSpawnPosition.transform.position.z));
                objectToActivate.transform.rotation = objectSpawnPosition.transform.rotation;
                objectToActivate.SetActive(true);
                // Debug.Log("spawn offset = " + spawnOffset);
            }
        }
    }

    IEnumerator DelayObjectPlacement()
    {
        Debug.Log("delay called");
        // print(Time.time);
        yield return new WaitForSeconds(5.0f);
        // print(Time.time);
                
    }


}
