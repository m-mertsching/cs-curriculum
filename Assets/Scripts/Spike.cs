using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spike : MonoBehaviour
{
    GameManger _gm;
    public SceneManager Current;
    public TopDown_AnimatorController animator;
    

    private void Start()
    {
        _gm = FindFirstObjectByType<GameManger>();
        animator = GetComponentInChildren<TopDown_AnimatorController>();
    }

    void Die()
    {
        SceneManager.LoadScene("Start");
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            _gm.health -= 1;
            print("we have " + _gm.health + " health ");
        }
        if (other.gameObject.CompareTag("bullet"))
        {
            _gm.health -= 1;
            print("we have " + _gm.health + " health ");
        }
        

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("cavewall") && animator.IsAttacking && _gm.HasAxe)
        {
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (_gm.health <= 1)
        {
            Die();
            _gm.health = 5;
        }
    }
}   
