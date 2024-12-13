using UnityEngine;

public class moster1 : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Distance at which the enemy detects the player
    public GameObject player; // Reference to the player's transform
    private bool movingForward = true;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float distanceToPlayer;
   
    private BoxCollider2D collider;
    GameManger _gm;
    public float health;
    private float cooldown = 1;
    public float interactionDistance = 2f;
    private Transform targetPoint;

    enum States
    {
        patrol,
        attack,
        die
    }
   
    private States state;

    private void Start()
    {
        health = 1;
        SwitchState(States.patrol);
        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            targetPoint = patrolPoints[currentPatrolIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (Input.GetMouseButton(0) && distanceToPlayer <= interactionDistance)
        {
            health -= 1;
        }
        
        if (state == States.patrol)
        {
            Patrol();
        }
        
        //if (state == States.attack)
        //{
            //Attack();
        //}
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
            
            if(state == States.patrol)
                SwitchState(States.attack);
            else
            {
                SwitchState(States.patrol);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (state == States.attack)
            {
                SwitchState(States.patrol);
            }
        }
    }
    
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // Move towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
       
        // Check if we've reached the current target point
        if (Vector2.Distance(transform.position, targetPoint.position) < 1f)
        {
            //print("is moving");
            // Switch direction
            if (movingForward)
            {
                
                if (currentPatrolIndex < patrolPoints.Length - 1)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex == patrolPoints.Length - 1)
                    {
                        //print("collided with point");
                        movingForward = false;
                       
                    }
                }
            }
            else
            {
                if (currentPatrolIndex > 0)
                {
                    currentPatrolIndex--;
                    if (currentPatrolIndex == 0)
                    {
                        movingForward = true;
                    }
                }
            }
           
            // Update target point
            targetPoint = patrolPoints[currentPatrolIndex];
        }
    }
    
    void AttackPlayer()
    {
        cooldown -= Time.deltaTime;
        //print("trying to attack");
        if (cooldown < 0)
        {
            //animator.IsAttacking = true;
            //animator.Attack();
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
    }


}
