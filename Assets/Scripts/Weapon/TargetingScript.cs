using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class TargetingScript : MonoBehaviour
{
    private EnemySpawn enemyHandler;

    [Header("TARGETING BOOLS")]
    [Space]
    [SerializeField]
    private bool 
        targetFirst, 
        targetLast, 
        targetStrong, 
        targetWeak, 
        targetClose;

    [Header("ENEMY ARRAY")]
    [SerializeField] private List<GameObject> _enemyArray; // List of GameObjects Active Enemy Array

    private void Start()
    {
        enemyHandler = EnemySpawn.Instance;
    }

    private void Update()
    {
        if (_enemyArray.Count > 0)
        {
            foreach (GameObject g in _enemyArray)
            {
                if (g == null)
                {
                    _enemyArray.Remove(g);
                }
            }
        }
    }

    public Transform TargetingMode()
    {

        // TARGETS FIRST ENEMY ON PATH 
        if (targetFirst)
        {
            List<GameObject> tempArray = _enemyArray;
            if (tempArray.Count > 0)
            {
                return _enemyArray[0].transform;
            }
            else return null;
        }
            

        // TARGETS LAST ENEMY ON PATH
        else if (targetLast)
        {
            List<GameObject> tempArray = _enemyArray;
   
            if (tempArray.Count > 0)
            {
                return _enemyArray[_enemyArray.Count - 1].transform;
            }
            else return null;
        }

        else if (targetStrong)
            return enemyHandler.GetStrongestEnemy().transform;

        else if (targetWeak)
            return enemyHandler.GetWeakestEnemy().transform;

        else if (targetClose)
        {
            List<GameObject> tempArray = enemyHandler.GetEnemyArray;
            GameObject closest = null;
            float shortestDistance = 100;
            if (tempArray.Count > 0)
            {
                foreach (GameObject g in tempArray)
                {
                    if (tempArray.IndexOf(g) == 0)
                    {
                        closest = g;
                        shortestDistance = Vector3.Distance(transform.position, closest.transform.position);
                    }
                    else if (Vector3.Distance(transform.position, g.transform.position) < shortestDistance)
                    {
                        closest = g;
                        shortestDistance = Vector3.Distance(transform.position, closest.transform.position);
                    }
                }

                return closest.transform;
            }
            else return null;
        }
        else
        {
            return null;
        }
    }

    public Transform TargetingMode(string InfiniteRange)
    {

        // TARGETS FIRST ENEMY ON PATH 
        if (targetFirst)
        {
            return enemyHandler.GetFarthestEnemyOnPath().transform;
        }
        // TARGETS LAST ENEMY ON PATH
        else if (targetLast)
        {
            return enemyHandler.GetShortestEnemyOnPath().transform;
        }

        else if (targetStrong)
            return enemyHandler.GetStrongestEnemy().transform;

        else if (targetWeak)
            return enemyHandler.GetWeakestEnemy().transform;

        else if (targetClose)
        {
            List<GameObject> tempArray = enemyHandler.GetEnemyArray;
            GameObject closest = null;
            float shortestDistance = 100;
            if (tempArray.Count > 0)
            {
                foreach (GameObject g in tempArray)
                {
                    if (tempArray.IndexOf(g) == 0)
                    {
                        closest = g;
                        shortestDistance = Vector3.Distance(transform.position, closest.transform.position);
                    }
                    else if (Vector3.Distance(transform.position, g.transform.position) < shortestDistance)
                    {
                        closest = g;
                        shortestDistance = Vector3.Distance(transform.position, closest.transform.position);
                    }
                }

                return closest.transform;
            }
            else return null;
        }
        else
        {
            return null;
        }
    }

    public void AddEnemy(GameObject enemy) => _enemyArray.Add(enemy);
    public void RemoveEnemy(GameObject enemy) => _enemyArray.Remove(enemy);
    public int GetEnemyArrayLength() => _enemyArray.Count;


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
    #endregion
}
