using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger gm;
    public int purse;

    private void Awake()
    {
        if(gm!=null && gm!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
        
}
