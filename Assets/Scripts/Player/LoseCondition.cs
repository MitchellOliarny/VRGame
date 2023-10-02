using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] private int playerHealth;



    #region EVENT SYSTEM SUBSCRIBING
    private void OnEnable()
    {
        EventManagerScript.OnEnemyReachEnd += OnEnemyReachEnd;
    }

    private void OnDisable()
    {
        EventManagerScript.OnEnemyReachEnd -= OnEnemyReachEnd;
    }
    #endregion

    private void OnEnemyReachEnd(int damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);
    }
}
