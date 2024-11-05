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
    public TopDown_AnimatorController playeranimator;
    public float interactionDistance = 3f;
    
    public Vector2 OGsize;
    private BoxCollider2D collider;
    GameManger _gm;
    private float cooldown = 1;
    public float health;
    public GameObject AxeItem;
   
    

    enum States
    {
        patrol,
        chase,
        attack,
        die
    }

    private States state;

    private void Start()
    {
        SwitchState(States.patrol);
        animator = GetComponentInChildren<TopDown_EnemyAnimator>();

        collider = GetComponent<BoxCollider2D>();
        OGsize = collider.size;
        _gm = FindFirstObjectByType<GameManger>();
        health = 1;
    }

    void Update()
    {
        
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetMouseButton(0) && distanceToPlayer <= interactionDistance)
        {
            health -= 1;
        }
        
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
        if (state == States.die)
        {
            Die();
        }

        if (health < 1)
        {
            Die();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
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

    void OnTriggerExit2D(Collider2D other)
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

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }


    void ChasePlayer()
    {
        collider.size = OGsize;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
      
    }


    void AttackPlayer()
    {
        cooldown -= Time.deltaTime;
        //print("trying to attack");
        if (cooldown < 0)
        {
            animator.IsAttacking = true;
            animator.Attack();
            collider.size = new Vector2(3, 3);
            //print("attacked");
            _gm.health -= 1;
            cooldown = 1;
        }
       
        
    }

    void SwitchState(States _state)
    {
        state = _state;
        
    }

    void Die()
    {
       print("i hit enemy");
       Destroy(gameObject);
       Instantiate(AxeItem, transform.position, Quaternion.identity);
        
    }
}