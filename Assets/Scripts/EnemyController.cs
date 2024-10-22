using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;




public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Distance at which the enemy detects the player
    public GameObject player; // Reference to the player's transform

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float distanceToPlayer;

    private void Start()
    {
        print(player);
    }

    void Update()
    {
        print(isChasing);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        if (isChasing)
        {
            ChasePlayer();
            
        }
        else
        {
            Patrol();

    /*
            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
            }
            */
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isChasing = false;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        //float step = speed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }


    private void ChasePlayer()
    {
        //float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        print("we got here");
        
        /*
        if (distanceToPlayer > detectionRange)
        {
            isChasing = false;
        }
        */
    }


    private void AttackPlayer()
    {
        // Add attack logic here (e.g., deal damage)
    }
}