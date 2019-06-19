using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovementBehaviour : MonoBehaviour
{

    // Movement speed of this terrain instance.
    public float terrainMovementSpeed;

    // Terrain life length of this instance.
    public float thisTerrainLife;

    // Stores a reference to the terrain manager.
    private GameObject theTerrainManager;
    
    // Called when terrain instantiates.
    void OnEnable ()
    {
        // Find the terrain spawner with tag.
        theTerrainManager = GameObject.FindGameObjectWithTag("TerrainSpawner");

        // Access the terrain manager script on the spawner and set the speed.
        terrainMovementSpeed = theTerrainManager.gameObject.GetComponent<TerrainManager>().terrainSpeed;

        // Access the terrain manager script and set the life length.
        thisTerrainLife = theTerrainManager.gameObject.GetComponent<TerrainManager>().terrainLife;

        // Start coroutine determining life of each instance
        StartCoroutine(LifeOfTerrainInstance());

    }

    // Update is called once per frame
    void Update()
    {
        // Move this instance of terrain.
        transform.Translate(Vector3.back * Time.deltaTime * terrainMovementSpeed);
    }

    IEnumerator LifeOfTerrainInstance()
    {
        // Wait for the life length of terrain then destroy it.
        yield return new WaitForSeconds(thisTerrainLife);
        Destroy(this.gameObject);
    }

}
