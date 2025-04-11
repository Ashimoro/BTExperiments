using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHP : MonoBehaviour
{

    // script that is updating player hp in ui

    public GameObject player;
    public TMP_Text playerHPText;
    private PlayerMovement _playerHP;



    void Start()
    {
        _playerHP = player.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "HP: "+ _playerHP.playerHP.ToString();
    }
}
