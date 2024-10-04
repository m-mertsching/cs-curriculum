using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    public Vector3 targetposition;
    
    void Start()
    {
        speed = 10;
        
    }



    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetposition, speed * Time.deltaTime);
    }



}