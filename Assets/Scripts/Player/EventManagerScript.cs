using System;
using UnityEngine;
using TMPro;


public class EventManagerScript : MonoBehaviour
{
    public static EventManagerScript Instance;

    [SerializeField] private float money;
    [SerializeField] private int startingCash;

    [SerializeField] private TextMeshProUGUI MoneyText;
    private void Awake()
    {
        Instance = this;
        money += startingCash;
        MoneyText.text = "Coins: " + money;
    }

    #region LOSE CONDITION
    public static event Action<int> OnEnemyReachEnd;
    public static void EnemyReachedEnd(int damage) { OnEnemyReachEnd?.Invoke(damage); }
    #endregion

    #region DAMAGE
    public static event Action<int, float, int> OnEnemyHit;
    public static void EnemyHit(int ID, float damage, int pierce) { OnEnemyHit?.Invoke(ID, damage, pierce); }

    public static event Action<int, int> OnBladeHit;
    public static void BladeHit(int ID, int SpinID) { OnBladeHit?.Invoke(ID, SpinID); }

    public static event Action<Collider, Vector3, int> OnBladeExit;
    public static void BladeExit(Collider other, Vector3 closestPoint, int SpinID) { OnBladeExit?.Invoke(other, closestPoint, SpinID); }
    #endregion

    #region MONEY

    public float GetMoney => money;
    public void MoneyDrop(float amount) { money += amount; MoneyText.text = "Coins: " + money; }

    public void MoneyReduce(float amount) { money -= amount; MoneyText.text = "Coins: " + money; }
    #endregion

    #region SHOP
    
    #endregion
}

