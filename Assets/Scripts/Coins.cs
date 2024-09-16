using UnityEngine;

public class Coins : MonoBehaviour
{
   GameManger gm;
    public bool overworld;
    private void Start()
    {
        gm = FindObjectOfType<GameManger>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("I triggered something");

        if (other.gameObject.CompareTag("Coin"))
        {
            // have Coins
            gm.purse = gm.purse + 1;

            print("we have " + gm.purse + " coins in our purse. ");
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
