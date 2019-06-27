using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovementBehaviour : MonoBehaviour
{

    // Movement speed of this terrain instance.
    public float terrainMovementSpeed;      

    // Stores a reference to the terrain manager.
    private GameObject theTerrainManager;    


    // Called when terrain instantiates.
    void OnEnable ()
    {
        // Find the terrain spawner with tag.
        theTerrainManager = GameObject.FindGameObjectWithTag("TerrainSpawner");

        // Access the terrain manager script on the spawner and set the speed.
        terrainMovementSpeed = theTerrainManager.gameObject.GetComponent<TerrainManager>().terrainSpeed;        
    }

    // FixedUpdate is used for physics updates
    void FixedUpdate()
    {
        // Move this instance of terrain.
        transform.Translate(Vector3.back * Time.deltaTime * terrainMovementSpeed);
    }
    

}
