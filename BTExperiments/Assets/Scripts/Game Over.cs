using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject player;
    public float winTime;
    public TMP_Text winTimer;

    private PlayerMovement _playerHP;
    private float _timer;

    void Start()
    {
        _playerHP = player.GetComponent<PlayerMovement>();
        _timer = winTime;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHP = _playerHP.playerHP;
        _timer -= Time.deltaTime;

        winTimer.text = "Survive " + ((int)_timer) + " more seconds";

        if (currentHP <= 0) {
            Application.Quit();
        }

        if (_timer <= 0){
            Application.Quit();
        }
        
    }
}
