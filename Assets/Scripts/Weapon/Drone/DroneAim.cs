using UnityEngine;
using System.Collections.Generic;
using PathCreation;

public class DroneAim : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private EnemySpawn enemyHandler; // Enemy Spawn Script
    [SerializeField] private DroneSettings droneSettings; // Drone Setting Scriptable
    private TargetingScript target; // Targeting Script

    [Header("DRONE ATTRIBUTES")]
    [SerializeField] private Transform _muzzle; // Muzzle Transform
    [SerializeField] private float range;


    [Header("TRANSFORM")]
    [SerializeField] private Transform drone; // Drone Transform
    [SerializeField] private float followDistance; // Following Distance
    [SerializeField] private float groundDistance; // Ground Distance
    [SerializeField] private float maxSpeed; // Maximum Speed
    [SerializeField] private Transform ground; // Ground Transform


    private void Start()
    {
        target = GetComponent<TargetingScript>(); // Sets Targeting script
        ground = GameObject.FindGameObjectWithTag("ground").GetComponent<Transform>(); // Sets ground to scene ground plane
        enemyHandler = EnemySpawn.Instance; // Sets Enemy Handler to EnemySpawn Instance

        followDistance = Random.Range(droneSettings.followDistanceMin, droneSettings.followDistanceMax); // Sets Follow Distance to a random range determined by Drone Settings
        groundDistance = Random.Range(droneSettings.groundDistanceMin, droneSettings.groundDistanceMax); // Sets Ground Distance to a random range determined by Drone Settings
        maxSpeed = droneSettings.maxSpeed; // Sets Max Speed of Drone determined by Drone Settings
    }

    private void FixedUpdate()
    {
        // If Drone's y position - ground's y position is less than Ground Distance
        if ((drone.position.y - ground.position.y) < groundDistance)
            // Drone position += Upwards Vector
            drone.position += Vector3.up;
    }

    private void Update()
    {
        // If there are enemies in the scene
        if (enemyHandler.GetEnemyArrayLength() >= 1)
        {
            // Point Muzzle towards target
            _muzzle.LookAt(target.TargetingMode());

            // If Distance from drone to target is greater than following Distance
            if (Vector3.Distance(drone.position, target.TargetingMode().position) > followDistance)
            {
                // Move drone towards enemy (Locks y axis)
                transform.position = new Vector3(
                    Vector3.MoveTowards(drone.position, target.TargetingMode().position, maxSpeed * Time.deltaTime).x,
                    drone.position.y,
                    Vector3.MoveTowards(drone.position, target.TargetingMode().position, maxSpeed * Time.deltaTime).z
                    );
            }
        }
    }
}
