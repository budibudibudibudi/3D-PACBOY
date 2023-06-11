using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZAGSTUDIO.GAME.PLAYER
{
    public class Player : MonoBehaviour
    {
        #region singleton
        public static Player Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion
        [SerializeField] private PLAYERSTATE state;
        [SerializeField] private int Health = 2;
        public int GetHealth() { return Health; }
        public PLAYERSTATE GetState() { return state; }
        public void SetPlayerState(PLAYERSTATE newState) 
        {
            state = newState;
            Actions.OnPlayerStateChange?.Invoke(state,0);
        }
        #region Subscription
        private void OnEnable()
        {
            Actions.OnGetBuah += OnGetBuah;
            Actions.OnPlayerStateChange += OnPlayerStateChange;
        }

        private void OnDisable()
        {
            Actions.OnGetBuah -= OnGetBuah;
            Actions.OnPlayerStateChange -= OnPlayerStateChange;
        }

        #endregion
        private void OnGetBuah()
        {
            SetPlayerState(PLAYERSTATE.BUFFED);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
                Actions.OnGetCoin?.Invoke(1);
            }
            if(other.CompareTag("Buah"))
            {
                other.gameObject.SetActive(false);
                Actions.OnGetBuah?.Invoke();
            }
            if(other.CompareTag("Enemy"))
            {
                switch (state)
                {
                    case PLAYERSTATE.NORMAL:
                        print("hitted");
                        Mathf.Clamp(Health -= 1,0,2);
                        if(Health <= 0)
                        {
                            Actions.OnStateChange.Invoke(STATE.GAMEOVER);
                        }
                        else
                        {
                            Actions.OnPlayerStateChange.Invoke(PLAYERSTATE.IMMUNE, Health);
                        }
                        break;
                    case PLAYERSTATE.BUFFED:
                        print("hitted");
                        other.GetComponent<ENEMY.EnemyController>().SetState(ENEMYSTATE.DEAD);
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnPlayerStateChange(PLAYERSTATE arg1, int arg2)
        {
            state = arg1;
            switch (state)
            {
                case PLAYERSTATE.NORMAL:
                    break;
                case PLAYERSTATE.IMMUNE:
                    StartCoroutine(Immune());
                    break;
                case PLAYERSTATE.BUFFED:
                    StartCoroutine(Buffed());
                    break;
                default:
                    break;
            }
        }

        private IEnumerator Buffed()
        {
            yield return new WaitForSeconds(5);
            Actions.OnPlayerStateChange.Invoke(PLAYERSTATE.NORMAL, 0);

        }

        private IEnumerator Immune()
        {
            yield return new WaitForSeconds(5);
            Actions.OnPlayerStateChange.Invoke(PLAYERSTATE.NORMAL,0);
        }
    }
}