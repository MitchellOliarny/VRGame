using UnityEngine;
using System.Collections.Generic;
using PathCreation.Examples;

[CreateAssetMenu]
public class EnemyWaveScriptable : ScriptableObject
{
    [SerializeField] private List<EnemySettings> SlimeTypes;
    [SerializeField] private List<EnemyModifierSettings> SlimeModifierArray;
    [SerializeField] private float waveTimer;

    public List<EnemySettings> GetWaveContents() => new List<EnemySettings>(SlimeTypes);
    public List<EnemyModifierSettings> GetEnemyModifiers() => new List<EnemyModifierSettings>(SlimeModifierArray);
    public float GetWaveTimer { get { return waveTimer; } }
}