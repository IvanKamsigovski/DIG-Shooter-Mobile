using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private PoolManager pooler;

    private void Start()
    {
        pooler = PoolManager.Instance;
    }

    private void OnEnable()
    {
        EventHolder.OnShoot += Fire;
    }

    private void OnDisable()
    {
        EventHolder.OnShoot -= Fire;
    }

    public void Fire(Transform projectileOrigin)
    {
        //var projectile = poolManager.Get();
        var projectile = pooler.Get("Bullet");
        //projectile.transform.SetParent(gameObject.transform);
        projectile.transform.rotation = projectileOrigin.rotation;
        projectile.transform.position = projectileOrigin.position;
        projectile.gameObject.SetActive(true);
    }
}
