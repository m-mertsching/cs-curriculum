using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    private float xVector;
    private float xDirection;
    public float ySpeed;
    private float yVector;
    private float yDirection;

    private Rigidbody2D rb;

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
        }


        xSpeed = 5f;
        xVector = 0f;
        xDirection = 0;

        ySpeed = 5f;
        yVector = 0f;
        yDirection = 0f;
    }

    private void Update()
    {
        xDirection = Input.GetAxis("Horizontal");
        xVector = xSpeed * xDirection * Time.deltaTime;

        transform.Translate(xVector, 0, 0);

        yDirection = Input.GetAxis("Vertical");
        yVector = ySpeed * yDirection * Time.deltaTime;

        transform.Translate(0, yVector, 0);

    }
    
    //for organization, put other built-in Unity functions here
    
    
    
    
    
    //after all Unity functions, your own functions can go here
}