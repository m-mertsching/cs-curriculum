using UnityEngine;

public class Coins : MonoBehaviour
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

        if (other.gameObject.CompareTag("Coin"))
        {
            // have Coins
            _gm.purse = _gm.purse + 1;

            print("we have " + _gm.purse + " coins in our purse. ");
            Destroy(other.gameObject);
        }

    }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                print("we hit a wall");
            }
        }

    


}
