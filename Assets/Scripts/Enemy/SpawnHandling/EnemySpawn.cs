using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using TMPro;
public class EnemySpawn : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] public static EnemySpawn Instance; // Instance of This Script
    [SerializeField] private WaveHandler handler; // Wave Handler
    [SerializeField] private EventManagerScript manager; // Wave Handler

    [Header("TRANSFORM ARRAY")]
    [SerializeField] private PathCreator path; // Path Creator

    [Header("ENEMY ARRAY")]
    [SerializeField] private GameObject Slime;
    [SerializeField] private List<EnemySettings> _enemyHolderArray; // List of GameObjects Temp Holder Array
    [SerializeField] private List<GameObject> _enemyArray; // List of GameObjects Active Enemy Array

    [Header("MODIFY ARRAY")]
    [SerializeField] private List<EnemyModifierSettings> modifierArray; // Modifier List

    [Header("COOLDOWN")]
    [SerializeField] private float enemySpawnTimer; // Enemy Spawn Timer
    [SerializeField] private float startingEnemyTimer; // Starting Enemy Timer

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI WavesText;


    private int index = 0;

    private void Awake()
    {
        Instance = this; // Set Instance to this
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        handler.WaveCounter(0); // Set Wave Count to zero
        WavesText.text = "Wave #" + (handler.GetActiveWave() + 1);
        path = GameObject.FindGameObjectWithTag("path").GetComponent<PathCreator>(); // Set Path to scene path

        _enemyHolderArray = handler.GetEnemiesForWave(); // Add Wave Handler Slimes to Holder Array
        startingEnemyTimer = handler.GetWaveTimer(); // Set Timer to Wave Handler Timer
        modifierArray.AddRange(handler.GetEnemyModifiers()); // Set Modifier Array to Wave Handler Modifier Array

        enemySpawnTimer = startingEnemyTimer; // Set EnemySpawnTimer to Starting Timer

    }

    void Update()
    {
        if (handler.GetMaxWave < handler.GetActiveWave() + 1) return;

        // If Enemy Array is Empty && Enemy Holder Array is Empty && Modifier Array is Empty
        if (_enemyArray.Count <= 0 && _enemyHolderArray.Count <= 0 && modifierArray.Count <= 0)
        {
            handler.WaveCounter(handler.GetActiveWave() + 1); // Add one to Wave Counter
            manager.MoneyDrop(handler.GetWaveCash);

            WavesText.text = "Wave #" + (handler.GetActiveWave() + 1);
            _enemyHolderArray.AddRange(handler.GetEnemiesForWave()); // Add Wave Handler Slimes to Holder Array
            startingEnemyTimer = handler.GetWaveTimer(); // Set Timer to Wave Hanlder Timer
            modifierArray.AddRange(handler.GetEnemyModifiers()); // Set Modifier Array to Wave Handler Modifier Array

            index = 0;

        }
        // Else If Spawn Timer is less than equal 0
        else if (enemySpawnTimer <= 0)
        {

            enemySpawnTimer = startingEnemyTimer; // Set enemy spawn Timer to starting Timer

            // If Enemy Holder Array has more than 0
            if (_enemyHolderArray.Count > 0)
            {
                GameObject tempEnemy = Instantiate(Slime, path.path.GetPoint(0), Quaternion.identity); // Instantiate enemyHolderArray[0] gameObject at path index 0
                tempEnemy.GetComponent<EnemyArrayIndex>().Index = index;
                tempEnemy.GetComponent<PathFollower>().settings = _enemyHolderArray[0];
                if (modifierArray.Count > 0)
                {
                    if (modifierArray[0])
                    {
                        tempEnemy.GetComponentInChildren<EnemyModifiers>().SetModifiers(modifierArray[0]); // Set modifier of newly created enemy
                        modifierArray.Remove(modifierArray[0]); // Remove most recent Modifier from modifier array 
                    }
                }

                _enemyHolderArray.Remove(_enemyHolderArray[0]); // Remove most recent enemy from enemy holder array

                _enemyArray.Add(tempEnemy); // Add enemy to enemy array

                index++;
            }
        }
        // If enemySpawnTimer greater than 0
        else
            enemySpawnTimer -= Time.deltaTime; // SpawnTimer -= Time
    }

    #region TARGETING: GetFarthestEnemyOnPath(); GetShortestEnemyOnPath(); GetStrongestEnemy(); GetWeakestEnemy(); 

    //-- GET FARTHEST ENEMY ON PATH --\\
    public GameObject GetFarthestEnemyOnPath()
    {
        GameObject farthest = null; // Sets temp GameObject

        // Runs through enemyArray
        foreach (GameObject g in _enemyArray)
        {
            // If first run through set farthest to first index
            if (_enemyArray.IndexOf(g) == 0) farthest = g;

            // Else If enemyArray[i] Distance is greater than farthest GameObject Distance
            else if (g.GetComponent<PathFollower>().GetDistanceTravelled > farthest.GetComponent<PathFollower>().GetDistanceTravelled)
                farthest = g; // Set farthest to enemyArray[i]
        }
        return farthest; // Return Farthest
    }

    //-- GET SHORTEST ENEMY ON PATH --\\
    public GameObject GetShortestEnemyOnPath()
    {
        GameObject shortest = null; // Sets temp GameObject 

        // Runs through enemyArray
        foreach (GameObject g in _enemyArray)
        {
            // If first run through set shortest to first index
            if (_enemyArray.IndexOf(g) == 0) shortest = g;

            // Else If enemyArray[i] Distance is lower than shortest GameObject Distance
            else if (g.GetComponent<PathFollower>().GetDistanceTravelled < shortest.GetComponent<PathFollower>().GetDistanceTravelled)
                shortest = g; // Set shortest to enemyArray[i]
        }
        return shortest; // Return Shortest
    }

    //-- GET STRONGEST ENEMY --\\
    public GameObject GetStrongestEnemy()
    {
        List<GameObject> reverseArray = _enemyArray; // Reverses the Array Order to search farthest first
        reverseArray.Reverse(); // Reverse Array
        GameObject strongest = null; // Sets temp GameObject

        // Runs through reverseArray
        foreach (GameObject g in reverseArray)
        {
            // If first run through set strongest to first index
            if (reverseArray.IndexOf(g) == 0) strongest = g;

            // Else If reverseArray[i] Health is greater than strongest GameObject Health
            else if (g.GetComponentInChildren<Health>().GetCurrentHealth() > strongest.GetComponentInChildren<Health>().GetCurrentHealth())
                strongest = g; // Set Strongest to reverseArray[i]
        }
        return strongest; // Return Strongest
    }

    //-- GET WEAKEST ENEMY --\\
    public GameObject GetWeakestEnemy()
    {
        List<GameObject> reverseArray = _enemyArray; // Reverses the Array Order to search farthest first
        reverseArray.Reverse(); // Reverse Array
        GameObject weakest = null; // Sets temp GameObject

        // Runs through reverseArray
        foreach (GameObject g in reverseArray)
        {
            // If first run through set weakest to first index
            if (reverseArray.IndexOf(g) == 0) weakest = g;

            // Else If reverseArray[i] Health is less than weakest GameObject Health
            else if (g.GetComponentInChildren<Health>().GetCurrentHealth() < weakest.GetComponentInChildren<Health>().GetCurrentHealth())
                weakest = g; // Set Weakest to reverseArray[i]
        }
        return weakest; // Return Weakest
    }

    #endregion

    #region GETTERS && SETTERS: GetEnemyArray; GetEnemyInArray(); GetEnemyArrayLength(); RemoveFromArray();

    //-- GET ENEMY ARRAY --\\
    // Returns List of enemyArray
    public List<GameObject> GetEnemyArray => _enemyArray;

    //-- GET ENEMY IN ARRAY --\\
    // INT: index of List
    // Returns enemyArray[i] GameObject
    public GameObject GetEnemyInArray(int i) => _enemyArray[i];

    //-- GET ENEMY ARRAY LENGTH --\\
    // Returns Length of enemyArray
    public int GetEnemyArrayLength() => _enemyArray.Count;

    //-- REMOVE FROM ARRAY --\\
    // GAMEOBJECT: Object wanting removed from enemyArray
    public void RemoveFromArray(GameObject enemy)
    {
        // If enemy is not null && enemyArray Contains enemy GameObject
        if (enemy != null && _enemyArray.Contains(enemy))
            _enemyArray.Remove(enemy); // Remove enemy from enemyArray
    }

    //-- ADD TO ARRAY --\\
    // GAMEOBJECT: Object wanting to add to enemyArray
    public void AddToArray(GameObject enemy)
    {
        if (enemy != null)
            _enemyArray.Add(enemy);
    }
    #endregion
}