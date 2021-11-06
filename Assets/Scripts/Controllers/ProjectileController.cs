using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IGameobjectPooled
{
    #region PoolSetup
    private PoolManager pool;

    public PoolManager Pool {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("Bad Pool");
        } 
    }
    #endregion

    [SerializeField] private float timeToLive;
    [SerializeField] private float bulletSpeed;

    private WaitForSeconds waitToDie;

    private void Awake()
    {
        waitToDie = new WaitForSeconds(timeToLive);
    }

    private void OnEnable()
    {
        StartCoroutine(destroyBullet());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private IEnumerator destroyBullet()
    {
        yield return waitToDie;
        //pool.ReturnToPool(this.gameObject);
        pool.ReturnToPool(this.gameObject, "Bullet");
    }

    private void OnTriggerEnter(Collider colider)
    {
        EnemyController enemy = colider.GetComponent<EnemyController>();
        

        if(enemy != null)
        {
            enemy.Damaged();
        }

        //pool.ReturnToPool(this.gameObject);
        pool.ReturnToPool(this.gameObject, "Bullet");

    }
}
