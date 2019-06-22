using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    // Used to check the tag of the object to be turned off.
    public string objectTag;

    // Reference to the spawn position
    public Transform spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("trigger entered");

        if (other.tag == objectTag)
        {
            // Debug.Log("tag is true");
            // other.gameObject.SetActive(false);
            GameObject objectToActivate = ObjectPooler.SharedInstance.GetPooledObject("Terrain");
            if (objectToActivate != null)
            {
                objectToActivate.transform.position = spawnPosition.transform.position;
                objectToActivate.transform.rotation = spawnPosition.transform.rotation;
                objectToActivate.SetActive(true);
            }
        }
    }
}
