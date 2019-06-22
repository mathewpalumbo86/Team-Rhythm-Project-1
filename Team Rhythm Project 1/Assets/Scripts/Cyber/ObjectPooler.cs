using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

// Individual object pool constructor class
public class ObjectPoolItem
{    
  public GameObject objectToPool;   // The game object to pool
  public int amountToPool;        // How many to instantiate to the pool on start
  public bool shouldExpand;         // Whether the pool should expand or not
}

// Object pool behaviour
public class ObjectPooler : MonoBehaviour {

  public static ObjectPooler SharedInstance;    // This instance of the generic pooler
  public List<ObjectPoolItem> itemsToPool;      // The list of object pool instances (so this code can be reused by different objects)
  public List<GameObject> pooledObjects;        // The object pool

	void Awake()
    {
        // Sets the ShareInstance to this instance of the object pooler
        SharedInstance = this;
	}

	// Use this for initialization
  void Start ()
  {
        // Sets up the object pool list
        pooledObjects = new List<GameObject>();
    
        //  Checks the list of objects to be pooled for objects of the correct type. item refers to an item instance
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // For the object pool size
            for (int i = 0; i < item.amountToPool; i++)
            {
                // Instantiates them all, turns them off and adds them to the object pool
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
  }
	
  // This searches for an inactive object in the pool that can be used. This is called by other scripts that need an object and requires a tag to be specified.
  public GameObject GetPooledObject(string tag)
  {
    //  Scans through the pool
    for (int i = 0; i < pooledObjects.Count; i++)
    {
      // Looks for inactive objects
      if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
      {
        return pooledObjects[i];
      }
    }
    
    // This checks if the object pool needs to expand and if it does adds another to the pool and expands the pool
    foreach (ObjectPoolItem item in itemsToPool)
    {
      if (item.objectToPool.tag == tag)
      {
        if (item.shouldExpand)
        {
          GameObject obj = (GameObject)Instantiate(item.objectToPool);
          obj.SetActive(false);
          pooledObjects.Add(obj);
          return obj;
        }
      }
    }
    return null;
  }

	// Update is called once per frame
	void Update () {
	
	}
}
