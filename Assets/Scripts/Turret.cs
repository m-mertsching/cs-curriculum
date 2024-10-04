using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public GameObject Projectile;
    private GameObject Turret_Projectile;
    private float firerate = 2;
    private float cooldown;
    private GameObject target = null;
    GameManger _gm;
    
    

    private void OnTriggerEnter2D(Collider2D other)
    { 
        
        if (other.gameObject.CompareTag("Player") )
        {
            print("we collided with a player");
            target = other.gameObject;
            

        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.gameObject.CompareTag("Player") )
        {
            target = null;
           

        }
    }

    
    void Update()
    {
        //every second cooldown go down by 1
        cooldown -= Time.deltaTime;
        if (cooldown < 0 && target != null)
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject Clone = Instantiate(Projectile, transform.position, Quaternion.identity);
        Projectile CloneScript = Clone.GetComponent<Projectile>();
        CloneScript.targetposition = target.transform.position;
        cooldown = firerate;
    }
}

