using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private PoolManager pooler;
    [SerializeField] private List<Transform> spawnPoints;

    private void Start()
    {
        pooler = PoolManager.Instance;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var projectile = pooler.Get("Enemy");
            projectile.transform.rotation = spawnPoints[i].rotation;
            projectile.transform.position = spawnPoints[i].position;
            projectile.gameObject.SetActive(true);
        }

    }
}
