using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovementBehaviour : MonoBehaviour
{

    // Movement speed of this terrain instance.
    public float terrainMovementSpeed;
        
    // Stores a reference to the terrain manager.
    private GameObject theTerrainManager;
    
    // Called when terrain object is turned on.
    void OnEnable ()
    {
        // Find the terrain spawner with tag.
        theTerrainManager = GameObject.FindGameObjectWithTag("TerrainSpawner");

        // Access the terrain manager script on the spawner and set the speed.
        terrainMovementSpeed = theTerrainManager.gameObject.GetComponent<TerrainManager>().terrainSpeed;        

       

    }

    // Update is called once per frame
    void Update()
    {
        // Move this instance of terrain.
        transform.Translate(Vector3.back * Time.deltaTime * terrainMovementSpeed);
    }

    


}
