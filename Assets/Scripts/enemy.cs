using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    private GameObject target;
    private int counter;
    public List<GameObject> waypoints = new List<GameObject>();
    
    private float speed;
    public Vector3 targetposition;
    
    void Start()
    {
        counter = 0;
        speed = 2;
        targetposition = waypoints[counter].transform.position;

    }



    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetposition, speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (other.gameObject.CompareTag("waypoint") && collider.gameObject.CompareTag("Enemy"))
        {
            counter += 1;
            if (counter == waypoints.Count)
            {
                counter = 0;
            }
            print(counter);
            targetposition = waypoints[counter].transform.position;
        }
    }
}
