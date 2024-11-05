using UnityEngine;

public class Coins : MonoBehaviour
{
   GameManger _gm;
   public TopDown_AnimatorController animator;
    private void Start()
    {
        _gm = FindFirstObjectByType<GameManger>();
        animator = GetComponentInChildren<TopDown_AnimatorController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // have Coins
            _gm.purse = _gm.purse + 1;

            print("we have " + _gm.purse + " coins in our purse. ");
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Axe"))
        {
            _gm.HasAxe = true;
            animator.SwitchToAxe();
            Destroy(other.gameObject);
        }

    }
  
     

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("cavewall") && Input.GetMouseButton(0))
            {
                print("we hit a wall");
            }
        }

    


}
