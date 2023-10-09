using System;
using UnityEngine;
using TMPro;


public class EventManagerScript : MonoBehaviour
{
    public static EventManagerScript Instance;

    [SerializeField] private int money;
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
    public static event Action<int, float> OnEnemyHit;
    public static void EnemyHit(int ID, float damage) { OnEnemyHit?.Invoke(ID, damage); }

    public static event Action<int, int> OnBladeHit;
    public static void BladeHit(int ID, int SpinID) { OnBladeHit?.Invoke(ID, SpinID); }

    public static event Action<Collider, Vector3, int> OnBladeExit;
    public static void BladeExit(Collider other, Vector3 closestPoint, int SpinID) { OnBladeExit?.Invoke(other, closestPoint, SpinID); }
    #endregion

    #region MONEY
    public void MoneyDrop(int amount) { money += amount; MoneyText.text = "Coins: " + money; }

    public void MoneyReduce(int amount) { money -= amount; MoneyText.text = "Coins: " + money; }
    #endregion

    #region SHOP
    
    #endregion
}

