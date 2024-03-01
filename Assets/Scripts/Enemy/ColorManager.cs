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
    public EnemySettings DecreaseColor(int tier, int pierce)
    {
        int newTier = tier - pierce;
        if (newTier > 0)
        {
            return _enemySettings[newTier - pierce];
        }
        else
        {
            return null;
        }
    }

    public Material GetMaterial(int tier, int pierce) => colors[(tier - pierce)];

}
