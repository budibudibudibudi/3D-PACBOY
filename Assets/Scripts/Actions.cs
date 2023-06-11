using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public static Action OnGetBuah;
    public static Action<int> OnGetCoin;
    public static Action<int> TotalCoin;
    public static Action<STATE> OnStateChange;
    public static Action<PLAYERSTATE,int> OnPlayerStateChange;
    public static Action<ENEMYSTATE> OnEnemyStateChange;
}
