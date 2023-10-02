using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptableDatabase : MonoBehaviour
{
    public static EnemyScriptableDatabase Instance;

    [Header("ENEMY SETTINGS SCRIPTABLES")]
    [SerializeField] private EnemySettings[] ENEMYSLIMESETTINGSDATABASE;
    [SerializeField] private EnemySettings[] ENEMYOGRESETTINGSDATABASE;

    private void Awake()
    {
        Instance = this;
    }

    // RETURNS SPECIFIC ENEMY SETTING SCRIPTABLE
    // NOTE: Parameter index is the level wanted returned
    // IF INDEX is 1, returns 0 index of array
    public EnemySettings GetEnemySetting(int index, string enemyType)
    {
        enemyType.ToUpper();
        switch (enemyType)
        {
            case "SLIME": return ENEMYSLIMESETTINGSDATABASE[index - 1];
            case "OGRE": return ENEMYOGRESETTINGSDATABASE[index - 1];
            default: Debug.LogError("Input: " + index + " as value wanted returned, value either too low or does not exist \n" +
                "Input: " + enemyType + " as enemy wanted returned, value does not match enemy type \n" +
                "RETURNING NULL VALUE");
                return null;
        }
    }
}
