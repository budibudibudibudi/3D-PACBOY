using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZAGSTUDIO.GAME.ENEMY;

namespace ZAGSTUDIO.GAME
{
    public class GameSetting : MonoBehaviour
    {
        #region Singleton
        public static GameSetting Instance { get; private set; }
        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }
        #endregion
        [SerializeField] private Transform buahParent;
        [SerializeField] private GameObject[] buahs;
        [SerializeField] private Transform titikPointsParent;
        Transform[] titikPoints;


        [SerializeField] private EnemyController[] enemies;
        [SerializeField]private int indexEnemies;

        [SerializeField] private Transform coinParent;
        private float coinsInGame;

        private void Start()
        {
            coinsInGame = coinParent.childCount;
            InitSpawnBuah();
            InitSpawnLocBuah();
            InvokeRepeating("UnlockEnemies",1,10);
            StartCoroutine(SpawnBuah());
        }

        private void UnlockEnemies()
        {
            if(indexEnemies <= enemies.Length-1)
            {
                enemies[indexEnemies].GetComponent<EnemyController>().SetState(ENEMYSTATE.PATROL);
                indexEnemies++;
            }
            else
            {
                CancelInvoke("UnlockEnemies");
            }
        }

        private void OnEnable()
        {
            Actions.OnGetBuah += OnGetBuah;
            Actions.OnGetCoin += OnGetCoin;
        }


        private void OnDisable()
        {
            Actions.OnGetBuah -= OnGetBuah;
            Actions.OnGetCoin -= OnGetCoin;

        }
        private void OnGetCoin(int obj)
        {
            coinsInGame -= obj;
            if(coinsInGame<= 0)
            {
                Actions.OnStateChange.Invoke(STATE.WIN);
            }
        }
        private void OnGetBuah()
        {
            StartCoroutine(SpawnBuah());
        }
        private void InitSpawnLocBuah()
        {
            titikPoints = new Transform[titikPointsParent.childCount];
            for (int i = 0; i < titikPoints.Length; i++)
            {
                titikPoints[i] = titikPointsParent.GetChild(i);
            }
        }
        private void InitSpawnBuah()
        {
            for (int i = 0; i < buahs.Length; i++)
            {
                GameObject go = Instantiate(buahs[i], buahParent);
                go.SetActive(false);
            }
        }

        private IEnumerator SpawnBuah()
        {
            yield return new WaitForSeconds(5);
            GameObject go = GetBuah();
            if (!go)
                GetBuah();
            else
            {
                go.SetActive(true);
                go.transform.position = titikPoints[UnityEngine.Random.Range(0, titikPoints.Length)].position;
            }
        }
        private GameObject GetBuah()
        {
            int random = UnityEngine.Random.Range(0, buahs.Length);
            if(!buahParent.GetChild(random).gameObject.activeInHierarchy)
            {
                return buahParent.GetChild(random).gameObject;
            }
            return null;
        }

    }


}