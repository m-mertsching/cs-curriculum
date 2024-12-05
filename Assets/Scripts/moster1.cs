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

    enum States
    {
        patrol,
        attack,
        die
    }
    
    private States state;

    private void Start()
    { 
        SwitchState(States.patrol);
        if (patrolPoints.Length > 0)
        {
            // Start patrolling if patrol points are set
            currentPatrolIndex = 0;
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
        currentPatrolIndex = 1;
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        print("heading towared" + currentPatrolIndex);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            print("switch direction");
            //Debug.Log($"Current Patrol Index: {currentPatrolIndex}, Moving Forward: {movingForward}");
            
            if (movingForward)
            {
               
                    movingForward = false;
                    currentPatrolIndex = 1;
            }
            
            else
            {
               
                    currentPatrolIndex = 0;
                    movingForward = true;
            }
            
            //currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
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
