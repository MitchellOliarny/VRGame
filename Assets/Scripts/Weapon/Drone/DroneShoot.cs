using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private DroneSettings droneSettings; // Drone Settings Scriptable

    [Header("SHOOTING VARIABLES")]
    [SerializeField] private Transform _muzzle; // Transform of Muzzle
    private float damage; // Drone Damage
    [SerializeField] int pierce; //Drone Pierce

    [Header("PARTICLE SYSTEM")]
    [SerializeField] private ParticleSystem droneMuzzleFire; // Particle System at Muzzle
    [SerializeField] private GameObject impactDroneFire; // Particle System on Impact

    [Header("FIRERATE")]
    private float fireRateTimer; // Fire Rate Timer
    private float fireRateStartTimer; // Fire Rate Starting Timer

    [Header("SOUND")]
    [SerializeField] private AudioSource shootSound;

    [Header("RAYCAST")]
    [SerializeField] private RaycastHit hit; // Raycast Hit
    [SerializeField] private Ray ray; // Ray

    private void Start()
    {
        damage = droneSettings.damage; // Damage is set to Drone Settings Damage
        fireRateStartTimer = droneSettings.fireRateStartTimer; // FireRateStartTimer is set to Drone Settings
        fireRateTimer = droneSettings.fireRateTimer; // FireRateTimer is set to Drone Settings
    }

    private void FixedUpdate()
    {
        // If Raycast collides
        // OUT: Muzzle
        // DIRECTION: Muzzle.Foward * 500
        // INFO: hit
        if(Physics.Raycast(_muzzle.position, _muzzle.TransformDirection(Vector3.forward) * 500, out hit))
        {
            // If Raycast collides with enemy
            if (hit.collider.tag == "enemy")
            {
                // Draw Debug
                // OUT: Muzzle
                // DIRECTION: Muzzle.Forward * Distance to Enemy
                // COLOR: Red
                Debug.DrawRay(_muzzle.position, _muzzle.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                // If fireRateTimer Less than equal to 0
                if (fireRateTimer <= 0) 
                {
                    if (hit.collider.gameObject.GetComponentInParent<EnemyArrayIndex>() != null)
                    EventManagerScript.EnemyHit(hit.collider.gameObject.GetComponentInParent<EnemyArrayIndex>().Index, damage, pierce);
                    shootSound.PlayOneShot(shootSound.clip, shootSound.volume);
                    droneMuzzleFire.Play(); // Play Drone Muzzle Fire Particle System
                    GameObject temp = Instantiate(impactDroneFire, hit.point, Quaternion.identity); // Instantiate impactDroneFire particle system at enemy.point
                    temp.transform.LookAt(_muzzle); // Make Impact Drone Fire Look At Drone for correct effect

                    fireRateTimer = fireRateStartTimer; // Reset Fire Rate Timer
                }
                // If fireRateTimer is not less than 0
                else fireRateTimer -= Time.deltaTime; // Subtract fireRateTimer -= Time
            }
        }
        // If Raycast does not collide
        else
            // Draw Ray
            // OUT: Muzzle
            // DIRECTION: Muzzle.Forward * 1000
            // COLOR: Green
            Debug.DrawRay(_muzzle.position, _muzzle.TransformDirection(Vector3.forward) * 1000, Color.green);

        // Subtract fireRateTimer -= Time
        fireRateTimer -= Time.deltaTime;
    }
}
