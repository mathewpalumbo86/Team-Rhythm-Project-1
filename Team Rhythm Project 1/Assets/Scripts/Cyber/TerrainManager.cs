using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    /// <summary>
    /// THIS NEEDS TO USE THE OBJECT POOLER
    /// CREATE ENOUGH TERRAIN FOR THE OBJECT POOL AND THEN STOP
    /// </summary>

        // Check this
    // ObjectPooler terrainPoolInstance;

    

    // Terrain prefab to be instantiated.
    public GameObject terrainPrefab;

    // How fast the terrain moves towards the player.
    public float terrainSpeed;
    
    // Timing values for instantiation.
    public float waitToStart;
    public float waitForNextTerrain;

    // Start is called before the first frame update
    void Start()
    {
        // TimedTerrainPlacement will be called after a delay, and then repeat after a different delay.
        InvokeRepeating("TimedTerrainPlacement", waitToStart, waitForNextTerrain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Instantiate the terrain prefabs when called.
     public void TimedTerrainPlacement()
    {
        Instantiate(terrainPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
    } 

}
