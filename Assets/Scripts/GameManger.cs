using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManger : MonoBehaviour
{
    private static GameManger _gm;
    public int purse;
    public int health;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;

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

    public void Start()
    {
        health = 5;
        coinText.text = "Coins :" + _gm.purse;
        healthText.text = "Health :" + _gm.health;
    }

    public void Update()
    {
        coinText.text = "Coins :" + _gm.purse;
        healthText.text = "Health :" + _gm.health;
    }
}
