using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    private float _xVector;
    private float _xDirection;
    public float ySpeed;
    private float _yVector;
    private float _yDirection;
    private Collider2D collider2D;
    public float length;

    private Rigidbody2D rb;
    //
    private int purse;
    private int health;
    
    public bool overworld; 
    public float jumpForce = 5f;  
    private bool isGrounded;       
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        xSpeed = 5f;
        _xVector = 0f;
        _xDirection = 0;

        ySpeed = 5f;
        _yVector = 0f;
        _yDirection = 0f;
        
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        collider2D = GetComponent<Collider2D>();
        
        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            ySpeed = 0;
        }


    }

    private void Update()
    {
        _xDirection = Input.GetAxis("Horizontal");
        _xVector = xSpeed * _xDirection * Time.deltaTime;

        transform.Translate(_xVector, 0, 0);

        _yDirection = Input.GetAxis("Vertical");
        _yVector = ySpeed * _yDirection * Time.deltaTime;

        transform.Translate(0, _yVector, 0);
        isGrounded = CheckIfGrounded();
        
        
        if (isGrounded && Input.GetKeyDown("space"))
        {
            Jump();
        }

        //if (Input.)
        //RaycastHit2D leftray = Physics2D.Raycast(new Vector2(transform.position.x - collider2D.bounds.x, transform.position.y))
        //RaycastHit2D rightray = Physics2D.Raycast(new Vector2(transform.position.x + collider2D.bounds.x, transform.position.y))    



    }

    //for organization, put other built-in Unity functions here
    private bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, collider2D.bounds.extents.y + 0.1f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
           // print("we hit a wall");
        }
    }


    //after all Unity functions, your own functions can go here
}