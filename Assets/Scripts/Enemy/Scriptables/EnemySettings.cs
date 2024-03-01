using UnityEngine;
[CreateAssetMenu]

public class EnemySettings : ScriptableObject
{
    [Header("HEALTH")]
    [SerializeField] private int tier;
    [SerializeField] private float scale;
    [SerializeField] private float health, maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float ChildLifeTime;
    [SerializeField] private int cashDrop;
    [SerializeField] private Material material;

    [SerializeField] private ParticleSystem deathParticle;


    public float GetMaxHealth => maxHealth;
    public float GetHealth => health;
    public float GetSpeed => speed;
    public int GetCashDrop => cashDrop;
    public Material GetMaterial => material;
    public float GetChildLifeTime => ChildLifeTime;

    public int GetTier => tier;

    public float GetScale => scale;

    public ParticleSystem GetDeathParticle => deathParticle;
}
