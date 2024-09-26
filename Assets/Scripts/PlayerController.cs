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

    private Rigidbody2D rb;
    //
    private int purse;
    private int health;
    public bool overworld; 

    private void Start()
    {
        // rb = GetComponent<Rigidbody2D>();

        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        
        
        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            ySpeed = 0;
        }


        xSpeed = 5f;
        _xVector = 0f;
        _xDirection = 0;

        ySpeed = 5f;
        _yVector = 0f;
        _yDirection = 0f;
    }

    private void Update()
    {
        _xDirection = Input.GetAxis("Horizontal");
        _xVector = xSpeed * _xDirection * Time.deltaTime;

        transform.Translate(_xVector, 0, 0);

        _yDirection = Input.GetAxis("Vertical");
        _yVector = ySpeed * _yDirection * Time.deltaTime;

        transform.Translate(0, _yVector, 0);

        

    }

    //for organization, put other built-in Unity functions here
   

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            print("we hit a wall");
        }
    }


    //after all Unity functions, your own functions can go here
}