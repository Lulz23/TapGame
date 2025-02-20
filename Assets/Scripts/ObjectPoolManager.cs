using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    
    public static List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform) { 
    
        PooledObjectInfo pool = objectPools.Find(p=> p.lookupString == objectToSpawn.name);

        if(pool == null)
        {

            pool = new PooledObjectInfo
            {
                lookupString = objectToSpawn.name,
            };
        }
            objectPools.Add(pool);

        GameObject poolObject = pool.inactveObjects.FirstOrDefault();
        if (poolObject == null)
        {
            poolObject = Instantiate(objectToSpawn, parentTransform);
        }
        else { 
        
            pool.inactveObjects.Remove(poolObject);
            poolObject.SetActive(true);
        }

        return poolObject;

    }

    public static void ReturnObjectToPool(GameObject gameObject) {

        string gameObjectName = gameObject.name.Substring(0, gameObject.name.Length - 7);

        PooledObjectInfo pool = objectPools.Find(p=> p.lookupString == gameObjectName);

        if (pool != null)
        {
            gameObject.SetActive(false);
            pool.inactveObjects.Add(gameObject);
        }

    }

}

public class PooledObjectInfo {


    public string lookupString;
    public List<GameObject> inactveObjects = new List<GameObject>();

}