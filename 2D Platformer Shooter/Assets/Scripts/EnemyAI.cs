using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Runtime.CompilerServices;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public Transform target;

    private enum EnemyType
    {
        melee,
        ranged,
        splitter
    }

    [Header("Specifications")]
    [SerializeField] EnemyType enemyType;
    [SerializeField] private float speed = 400f;
    [SerializeField] private float nextWaypointDistance = 1.5f;
    [SerializeField] private float minDistanceForPlayerDetection = 10f;

    //[Header("Fill Out Once")]

    [Header("Ranged Enemy Only")]
    [SerializeField] private LayerMask checkForCollisions;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firingDistance;
    private EnemyRanged enemyRanged;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GetTargetPlayer();
        InvokeRepeating("UpdatePath", 0f, .5f);
        if (enemyType == EnemyType.ranged)
        {
            enemyRanged = GetComponent<EnemyRanged>();
        }
    }

    private void Update()
    {
        if (target == null)
        {
            target = GetTargetPlayer();
        }
    }

    private void UpdatePath()
    {
        if(seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null || target == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        bool move = false;

        if (enemyType == EnemyType.melee)
        {
            move = true;

            
        }
        else if (enemyType == EnemyType.ranged)
        {
            enemyRanged.target = target;

            if (HasLineOfSightToTarget())
            {
                enemyRanged.fire = true;
                move = false;

                if (transform.position.x < target.transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                move = true;
            }
        }

        if (move)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private Transform GetTargetPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        foreach (GameObject player in allPlayers)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < shortestDistance && distanceToPlayer <= minDistanceForPlayerDetection)
            {
                shortestDistance = distanceToPlayer;
                targetPlayer = player;
            }
        }

        if (targetPlayer == null)
        {
            return null;
        }
        else
        {
            return targetPlayer.transform;
        }
    }

    private bool HasLineOfSightToTarget()
    {
        RaycastHit2D hitObsticle = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.forward, firingDistance, checkForCollisions);
        if (hitObsticle.transform == null)
        {
            return false;
        }
        if (hitObsticle.transform.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
