using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZAGSTUDIO.GAME.ENEMY
{
    public class EnemyController : MonoBehaviour
    {
        public Transform Player;
        NavMeshAgent agent;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(Player.position);
        }
    }
}