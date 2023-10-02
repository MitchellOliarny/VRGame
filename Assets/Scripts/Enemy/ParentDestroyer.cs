using UnityEngine;
using System.Collections;

public class ParentDestroyer : MonoBehaviour
{
    // Reference: https://tenor.com/view/doom-doom-slayer-doom-guy-doom-eternal-crucible-gif-19831598

    [SerializeField] private bool isChildDead; // Is Child Dead Boolean
    [SerializeField] private EnemySpawn enemySpawn; // Enemy Spawn Instance Script
    [SerializeField] private SlimeSpawn slimeSpawn;

    [SerializeField] private GameObject[] slimesToSpawnOnDeath;

    private void Start()
    {
        enemySpawn = EnemySpawn.Instance; // Sets enemySpawn to EnemySpawn Instance
    }

    private void Update()
    {

        // If parent gameobject has a child, return
        // If parent gameObject does not have a child set IsChildDead to true
        if (transform.childCount <= 0)
        {
            IsChildDead = true;
        }
    }

    //-- SETTERS --\\
    public bool IsChildDead
    {
        // Returns isChildDead
        get { return isChildDead; }
        set
        {
            // Sets isChildDead to value
            isChildDead = value;

            // if Child is Dead
            if (isChildDead)
            {
                enemySpawn.RemoveFromArray(gameObject); // Remove Parent Object from enemySpawn Array
                if (slimeSpawn != null) slimeSpawn.SpawnSlimeOnDeath(slimesToSpawnOnDeath, transform); // If SlimeSpawn is not null, spawn array of slimes at location of death
                transform.DetachChildren(); // Detach all children attatched to parent object
                Destroy(gameObject);
            }
        }
    }
}
