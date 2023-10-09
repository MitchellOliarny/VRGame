using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private TextMeshProUGUI LivesText;

    private void Start()
    {
        LivesText.text = "Lives: " + playerHealth;
    }

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
        LivesText.text = "Lives: " + playerHealth;
    }
}
