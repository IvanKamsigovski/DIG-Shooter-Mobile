using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; set; }

    public List<PooledObject> ObjectsToPool;

    //[SerializeField] private GameObject pooledObject;
    //[SerializeField] private int poolSize;
    //private Queue<GameObject> OLD = new Queue<GameObject>();

    private Dictionary<string,Queue<GameObject>> poolOfObjects;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolOfObjects = new Dictionary<string, Queue<GameObject>>();

        foreach (PooledObject item in ObjectsToPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject newObject = Instantiate(item.pooledObject);
                newObject.transform.parent = transform;
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);

                newObject.GetComponent<IGameobjectPooled>().Pool = this;
            }

            poolOfObjects.Add(item.tag, objectPool);
        }
    }

    //public GameObject Get()
    //{
    //    if (OLD.Count == 0)
    //        AddObjects();

    //    return OLD.Dequeue();
    //}

    //public void ReturnToPool(GameObject objectToReturn)
    //{
    //    objectToReturn.SetActive(false);
    //    OLD.Enqueue(objectToReturn);
    //}

    public void ReturnToPool(GameObject objectToReturn, string tag)
    {
        objectToReturn.SetActive(false);
        poolOfObjects[tag].Enqueue(objectToReturn);
    }

    public GameObject Get(string tag)
    {

        if(!poolOfObjects.ContainsKey(tag))
        {
            Debug.Log("Tag: " + tag + " not found in pool");
            return null;
        }

        if (poolOfObjects[tag].Count == 0)
            AddObjects(poolOfObjects[tag],tag);

        GameObject objectToSpawn = poolOfObjects[tag].Dequeue();

        return objectToSpawn;
    }

    public void AddObjects(Queue<GameObject> objectPool, string tag)
    {

        foreach (PooledObject item in ObjectsToPool)
        {

            if(item.tag == tag)
            {
                GameObject newObject = Instantiate(item.pooledObject);
                newObject.transform.parent = transform;
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);

                newObject.GetComponent<IGameobjectPooled>().Pool = this;
            }
        }
    }

    //private void AddObjects()
    //{
    //    foreach (PooledObject item in ObjectsToPool)
    //    {
    //        var newObject = GameObject.Instantiate(item.pooledObject);
    //        newObject.transform.parent = transform;
    //        newObject.SetActive(false);
    //        OLD.Enqueue(newObject);

    //        newObject.GetComponent<IGameobjectPooled>().Pool = this;

    //    }
    //}
}
