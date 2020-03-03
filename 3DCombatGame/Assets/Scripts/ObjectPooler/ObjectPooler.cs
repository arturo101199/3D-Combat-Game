using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler poolerInstance;
    public Pool[] pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        poolerInstance = this;
    }

    void Start()
    {
        CreatePools(); 
    }

    void CreatePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                queue.Enqueue(obj);
                obj.SetActive(false);
            }
            poolDictionary[pool.tag] = queue;
        }
    }

    public GameObject SpawnObject(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);
        IPooledObject pooledObject = obj.GetComponent<IPooledObject>();
        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(obj);
        return obj;
    }

}
