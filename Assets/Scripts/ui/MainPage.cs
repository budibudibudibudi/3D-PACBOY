using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace ZAGSTUDIO.GAME.UI
{
    public class MainPage : Page
    {
        [SerializeField] TMP_Text scoreTeks;
        [SerializeField] TMP_Text healthTeks;
        private void Start()
        {
            healthTeks.text = $"Health : { PLAYER.Player.Instance.GetHealth()}";
        }
        private void OnEnable()
        {
            Actions.TotalCoin += TotalCoin;
            Actions.OnPlayerStateChange += OnPlayerStateChange;
        }
        private void OnDisable()
        {
            Actions.OnGetCoin -= TotalCoin;
            Actions.OnPlayerStateChange -= OnPlayerStateChange;

        }

        private void OnPlayerStateChange(PLAYERSTATE arg1, int arg2)
        {
            healthTeks.text = $"Health : { PLAYER.Player.Instance.GetHealth()}";
        }

        private void TotalCoin(int obj)
        {
            scoreTeks.text = $"Score : {obj}";
        }

    }


}