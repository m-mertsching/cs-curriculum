using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Target;
    private GameObject Projectile;
    private float firerate = 2;
    private float cooldown;
    
    

    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.CompareTag("Player") && cooldown < 0)
        {
            GameObject Clone = Instantiate(Projectile);
            Projectile CloneScript = Clone.GetComponent<Projectile>();
            CloneScript.Target = other.transform.position;
            cooldown = firerate;
            
        }
    }

    
    void Update()
    {
        cooldown = Time.deltaTime;
    }
}

