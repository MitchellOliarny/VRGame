using UnityEngine;
using UnityEditor;

[CreateAssetMenu]

public class DroneSettings : ScriptableObject
{
    [Header("SHOOTING VARIABLES")]
    public float damage;
    public float speedMod;
    [Space]
    public Vector3 bulletScale;

    [Header("FIRERATE")]
    public float fireRateTimer;
    public float fireRateStartTimer;

    [Header("DRONE AIM")]
    public float followDistanceMin;
    public float followDistanceMax;
    [Space]
    public float groundDistanceMin;
    public float groundDistanceMax;
    [Space]
    public float maxSpeed;

    [Header("GAMEOBJECTS")]
    public GameObject bullet;
}
