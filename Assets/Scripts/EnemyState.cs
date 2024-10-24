using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;




public class EnemyState : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Distance at which the enemy detects the player
    public GameObject player; // Reference to the player's transform

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float distanceToPlayer;
    public TopDown_EnemyAnimator animator;
    

    enum States
    {
        patrol,
        chase,
        attack
    }

    private States state;
    
    private void Start()
    {
        SwitchState(States.patrol);
        animator = GetComponentInChildren<TopDown_EnemyAnimator>();
    }

    void Update()
    {
        
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (state == States.chase)
        {
            ChasePlayer();
        }

        if (state == States.patrol)
        {
            Patrol();
        }
        
        if (state == States.attack)
        {
            AttackPlayer();
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if(state == States.chase)
                SwitchState(States.attack);
            else
            {
                SwitchState(States.chase);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (state == States.attack)
            {
                SwitchState(States.chase);
            }
            else
            {
                SwitchState(States.patrol);
            }
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }


    private void ChasePlayer()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
      
    }


    private void AttackPlayer()
    {
        if (animator.IsAttacking)
        {
            animator.Attack();
            print("attacked");
        }

        animator.IsAttacking = true;
        animator.Attack();
        print("attacked");
    }

    void SwitchState(States _state)
    {
        state = _state;
    }

}