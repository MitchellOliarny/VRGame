using UnityEngine;

[CreateAssetMenu]

public class TurretSettings : ScriptableObject
{
    [Header("SHOOTING VARIABLES")]
    public float damage;
    public float bulletSpeed;
    public Vector3 bulletScale;
    public float speedMod;

    [Header("FIRERATE")]
    public float fireRateTimer;
    public float fireRateStartTimer;

    [Header("TURRET ATTRIBUTES")]
    public float accuracyMod;

}
