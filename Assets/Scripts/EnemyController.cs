using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ZAGSTUDIO.GAME.PLAYER;

namespace ZAGSTUDIO.GAME.ENEMY
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] ENEMYSTATE enemyState;
        public void SetState(ENEMYSTATE newstate) {
            enemyState = newstate; }

        [SerializeField] Transform Player;

        NavMeshAgent agent;

        [SerializeField] Transform[] Waypoints;
        [SerializeField] Transform targetWay;
        [SerializeField] Transform homeWay;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            int random = UnityEngine.Random.Range(0, Waypoints.Length);
            targetWay = Waypoints[random];

        }
        private void OnEnable()
        {
            Actions.OnEnemyStateChange += OnEnemyStateChange;
        }

        private void OnDisable()
        {
            Actions.OnEnemyStateChange -= OnEnemyStateChange;
        }

        private void Update()
        {
            switch (enemyState)
            {
                case ENEMYSTATE.PATROL:
                    agent.isStopped = false;
                    if (Mathf.Abs(Vector3.Distance(transform.position, targetWay.position)) <= 1)
                    {
                        print(Mathf.Abs(Vector3.Distance(transform.position, targetWay.position)));
                        int random = UnityEngine.Random.Range(0, Waypoints.Length);
                        targetWay = Waypoints[random];
                        agent.SetDestination(targetWay.position);
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, Player.position) < 3)
                        {
                            int random = UnityEngine.Random.Range(0, Waypoints.Length);
                            switch (Player.GetComponent<Player>().GetState())
                            {
                                case PLAYERSTATE.NORMAL:
                                    enemyState = ENEMYSTATE.CHASE;
                                    break;
                                case PLAYERSTATE.BUFFED:
                                    random = UnityEngine.Random.Range(0, Waypoints.Length);
                                    targetWay = Waypoints[random];
                                    agent.SetDestination(targetWay.position);
                                    break;
                                case PLAYERSTATE.IMMUNE:
                                    random = UnityEngine.Random.Range(0, Waypoints.Length);
                                    targetWay = Waypoints[random];
                                    agent.SetDestination(targetWay.position);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            agent.SetDestination(targetWay.position);
                        }

                    }
                    break;
                case ENEMYSTATE.CHASE:
                    agent.isStopped = false;
                    agent.SetDestination(Player.position);
                    break;
                case ENEMYSTATE.IDLE:
                    agent.isStopped = true;
                    break;
                case ENEMYSTATE.DEAD:
                    agent.isStopped = false;
                    targetWay = homeWay;
                    agent.SetDestination(targetWay.position);
                    if(Mathf.Abs(Vector3.Distance(transform.position, targetWay.position)) <= .5)
                        Actions.OnEnemyStateChange.Invoke(ENEMYSTATE.PATROL);
                    break;
                default:
                    break;
            }
        }
        private void OnEnemyStateChange(ENEMYSTATE obj)
        {
            enemyState = obj;
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(transform.position, 3);
        }
    }
}