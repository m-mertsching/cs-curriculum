using UnityEngine;

public class Spike : MonoBehaviour
{
    GameManger _gm;
    public bool overworld;

    private void Start()
    {
        _gm = FindObjectOfType<GameManger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("I triggered something");

        if (other.gameObject.CompareTag("Spikes"))
            // Start is called once before the first execution of Update after the MonoBehaviour is created
        {
            void Start()
            {
                _gm.health = _gm.health - 1;

                print("we have " + _gm.health + " lost health ");

            }
        }

        // Update is called once per frame
        void Update()
        {
        }

    }
}   