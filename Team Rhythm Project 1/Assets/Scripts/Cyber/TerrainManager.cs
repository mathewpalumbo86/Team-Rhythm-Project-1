using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    
    // How fast the terrain moves towards the player. Accessed by TerrainMovementBehaviour.cs
    public float terrainSpeed;    

    // Start is called before the first frame update
    void Start()
    {
        // NOTE: This scripts execution order must be later than the object pooler. The object pooler needs time to build the pool before it is accessed.
        
        // Places the very first terrain object. The object moves to an object activator which triggers the rest.
        GameObject thisTerrainPrefab = ObjectPooler.SharedInstance.GetPooledObject("Terrain");
        if (thisTerrainPrefab != null)
        {
            thisTerrainPrefab.transform.position = transform.position;            
            thisTerrainPrefab.transform.rotation = transform.rotation;
            thisTerrainPrefab.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
