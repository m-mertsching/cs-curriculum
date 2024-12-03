using UnityEngine;

public class moster1 : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Distance at which the enemy detects the player
    public GameObject player; // Reference to the player's transform

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
