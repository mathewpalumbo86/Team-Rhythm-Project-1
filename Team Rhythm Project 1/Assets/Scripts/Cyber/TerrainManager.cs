using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    // Terrain prefab to be instantiated.
    public GameObject terrainPrefab;

    // How fast the terrain moves towards the player.
    public float terrainSpeed;

    // Life length in seconds of the terrain.
    public float terrainLife;

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
