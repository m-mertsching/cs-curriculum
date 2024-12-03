using UnityEngine;

public class moster1animator : MonoBehaviour
{
    public bool IsAttacking { get; set; }

    Vector3 prevPos;
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }
}