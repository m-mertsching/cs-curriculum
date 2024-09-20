using System;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private static GameManger _gm;
    public int purse;
    public int health;

    private void Awake()
    {
        if(_gm!=null && _gm!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            _gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        health = 5;
    }
}
