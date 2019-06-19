using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObjectKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("trigger entered");

        // If the object that enters the terrain killzone is terrain turn it off.
        if (other.gameObject.tag == "Terrain")
        {
            // Debug.Log("triggered object is terrain");

            // THIS NEEDS TO BE REPLACED WITH A CALL TO THE OBJECT POOLER
            other.gameObject.SetActive(false);
        }
        

    }
}
