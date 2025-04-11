using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    // this script is related to both possible game end, death because of the enemy hits and win due to time out.

    public GameObject player;
    private PlayerMovement _playerHP;


    public float winTime;
    public TMP_Text winTimer;
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

        winTimer.text = "Survive " + ((int)_timer) + " more seconds"; // updating text on ui;

        if (currentHP <= 0) {
            Application.Quit(); // Right now I'm closing the game, because I didn't have time to work on UI game end screen
        }

        if (_timer <= 0){
            Application.Quit();
        }
        
    }
}
