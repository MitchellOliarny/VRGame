using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Header("VARIABLES")]
    [SerializeField] private Renderer rend;

    [Header("HEALTH COLORS")]
    [SerializeField] private Material[] colors;
    [SerializeField] private EnemySettings[] _enemySettings;
    public EnemySettings DecreaseColor(int tier)
    {
        int newTier = tier - 1;
        if (newTier > 0)
        {
            return _enemySettings[newTier - 1];
        }
        else
        {
            return null;
        }
    }

    public Material GetMaterial(int tier) => colors[(tier - 1)];

}
