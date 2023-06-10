using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Start()
        {
            InitSpawnBuah();
            StartCoroutine(SpawnBuah());
        }

        private void InitSpawnBuah()
        {
            foreach (var item in buahs)
            {
                Instantiate(item, buahParent);
            }
        }

        private void OnEnable()
        {
            Actions.onGetBuah += OnGetBuah;
        }

        private void OnGetBuah()
        {
            StartCoroutine(SpawnBuah());
        }
        private IEnumerator SpawnBuah()
        {
            yield return new WaitForSeconds(5);

        }

    }


}