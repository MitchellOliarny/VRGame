using System;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    public static EventManagerScript Instance;

    private void Awake()
    {
        Instance = this;
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
    public static event Action<int> OnMoneyDrop;
    public static void MoneyDrop(int money) { OnMoneyDrop?.Invoke(money); }

    public static event Action<int> OnMoneyReduce;
    public static void MoneyReduce(int money) { OnMoneyReduce?.Invoke(money); }
    #endregion

    #region SHOP
    public static event Action OnItemBuy;
    public static void ItemBuy() { OnItemBuy?.Invoke(); }
    #endregion
}
