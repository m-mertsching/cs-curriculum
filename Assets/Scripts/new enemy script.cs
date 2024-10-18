using System;
using UnityEngine;




public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Distance at which the enemy detects the player
    public Transform player; // Reference to the player's transform
    

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (isChasing)
        {
            ChasePlayer();
            if (distanceToPlayer > detectionRange)
            {
                isChasing = false;
            }
        }
        else
        {
            Patrol();


            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ;
        {
            isChasing = true;
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
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        print("we got here");
    }


    private void AttackPlayer()
    {
        // Add attack logic here (e.g., deal damage)
    }
}