using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PathCreation.Examples;

[CreateAssetMenu]

public class WaveHandler : ScriptableObject
{
    [Header("WAVE CREATOR")]

    [Header("PARAMETERS")]

    [SerializeField] private float SpawnTimer; // Wave Spawn Timer



    private int wave; // Wave counter

    [SerializeField] private List<GameObject> WaveArray; // List of Slimes
    [SerializeField] private float WaveTimer; // Wave Spawn Timer
    [SerializeField] private int waveCounter; // Wave Counter

    [SerializeField] private List<EnemyWaveScriptable> AllWaves; // List of Enemy Wave Scriptables which contain List of Slimes && List of Modifiers
    [SerializeField] private List<GameObject> currentWave;

    [Header("ENEMY INFO")]
    [Space]
    // SLIME PREFABS
    [SerializeField] private GameObject Slime;
    [SerializeField] private EnemySettings[] tiers;
    // OGRE PREFABS
    [SerializeField] private GameObject Ogre;

    [Header("ENEMY MOD INFO")]
    [Space]
    // ENEMY MODIFIER SCRIPTABLES
    [SerializeField] private EnemyModifierSettings Reinforced, Camo, Rock, Speed, MagicRes, ExploRes, None;


   

    #region GETTERS && SETTERS

    //-- GET ENEMIES FOR WAVE --\\
    public List<EnemySettings> GetEnemiesForWave() { return AllWaves[wave].GetWaveContents(); } 
    public List<EnemyModifierSettings> GetEnemyModifiers()  { return AllWaves[wave].GetEnemyModifiers();  }

    public float GetWaveTimer() {  return AllWaves[wave].GetWaveTimer; }
    //-- SET WAVE COUNTER --\\
    public void WaveCounter(int i) => wave = i;
    //-- GET ACTIVE WAVE --\\
    public int GetActiveWave() => wave;

    public int GetMaxWave { get { return AllWaves.Count; } }

    #endregion
}

//-- SLIME TYPE ENUM --\\
public enum SlimeType
{
    Slime_lvl1, Slime_lvl2, Slime_lvl3, Slime_lvl4, Slime_lvl5, Slime_lvl6, Slime_lvl7, Slime_lvl8,
    Ogre_lvl1
}

//-- SLIME MODIFIER ENUM --\\
public enum SlimeModifier
{
    None, Reinforced, Rock, Camo, Speed, MagicResist, ExplosionResist
}
