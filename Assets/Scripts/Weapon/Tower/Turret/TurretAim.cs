using UnityEngine;

public class TurretAim : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private EnemySpawn enemyHandler; // Enemy Spawn Instance
    [SerializeField] private TurretSettings turretSettings; // Turret Settings Scriptable
    private TargetingScript target; // Targeting Script

    [Header("TURRET ATTRIBUTES")]
    [SerializeField] private Transform _muzzle; // Muzzle of Turret


    private void Start()
    {
        enemyHandler = EnemySpawn.Instance; // Set enemyHandler to Enemy Spawn Instance
        target = GetComponent<TargetingScript>(); // Set Target to Targeting Script
    }

    void LateUpdate()
    {
        // If enemies are in the scene
       if(target.GetEnemyArrayLength() >= 1) 
            // Muzzle Look At returned target position
            // Muzzle will aim at the front half of the target
            _muzzle.LookAt(target.TargetingMode().position); 
    }
}
