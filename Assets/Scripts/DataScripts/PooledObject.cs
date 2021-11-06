using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public Transform spawnParent;
    public string tag;
    public GameObject pooledObject;
    public int poolSize;
    public bool expandable;
}
