using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IGameobjectPooled
{
    #region PoolSetup
    private PoolManager pool;

    public PoolManager Pool
    {
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
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrol
    private Vector3 walkPoint;
    private bool walPointSet;
    [SerializeField] private float walkPoinRange;

    //Atack
    public float TimeBetweanAttakcs;
    private bool alreadyAttack;
    private WaitForSeconds attackDelay;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    [SerializeField] Transform projectileOrigin;
    private PoolManager pooler;

    private void Start()
    {
        pooler = PoolManager.Instance;
        player = PlayerManager.Instance.Player;
        agent = GetComponent<NavMeshAgent>();
        attackDelay = new WaitForSeconds(TimeBetweanAttakcs);
    }
    #region AI
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) Attacking();
    }

    private void Patrolling()
    {
        if (!walPointSet) SearchWalkPoint();

        if (walPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPoinRange, walkPoinRange);
        float randomZ = Random.Range(-walkPoinRange, walkPoinRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);

        if(!alreadyAttack)
        {
            alreadyAttack = true;
            StartCoroutine(resetAttack());
        }
    }
    private IEnumerator resetAttack()
    {
        EventHolder.OnShoot?.Invoke(projectileOrigin);
        yield return attackDelay;
        alreadyAttack = false;
    }
    #endregion
    public void Damaged()
    {
        Debug.Log("Udaren");

        Destroy(gameObject);
    }
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    #endregion
}
