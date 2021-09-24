using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;

    private PoolManager pooler;

    private void Start()
    {
        pooler = PoolManager.Instance;
    }

    private void OnEnable()
    {
        PlayerManager.OnShoot += Fire;
    }

    private void OnDisable()
    {
        PlayerManager.OnShoot -= Fire;
    }

    public void Fire()
    {
        //var projectile = poolManager.Get();
        var projectile = pooler.Get("Bullet");
        //projectile.transform.SetParent(gameObject.transform);
        projectile.transform.rotation = transform.rotation;
        projectile.transform.position = transform.position;
        projectile.gameObject.SetActive(true);
    }
}
