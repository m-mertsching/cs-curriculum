using UnityEngine;

public class Spike : MonoBehaviour
{
    GameManger _gm;
    

    private void Start()
    {
        _gm = FindFirstObjectByType<GameManger>();
        
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            _gm.health -= 1;
            print("we have " + _gm.health + " health ");
        }

    }
}   