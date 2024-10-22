using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spike : MonoBehaviour
{
    GameManger _gm;
    public SceneManager Current;
    

    private void Start()
    {
        _gm = FindFirstObjectByType<GameManger>();
        
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            _gm.health -= 1;
            print("we have " + _gm.health + " health ");
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
