using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class OgreHealth : Health
{
    [Header("SCRIPTS")]
    [SerializeField] private EventManagerScript manager;
    [SerializeField] private DamageEvent damageEvent;
    [SerializeField] private EnemySpawn enemySpawn;
    [SerializeField] private MoneyDrop moneyDrop;
    [SerializeField] private EnemySettings settings;
    [SerializeField] private PathFollower follower;
    [SerializeField] private ParentDestroyer parentDestroyer;

    [Header("HEALTH")]
    [SerializeField] private float _maxHealth; // Max health of entity
    [SerializeField] private float currentHealth; // Health holder value
    [SerializeField] private int damage;

    [SerializeField] private float ChildLifeTime;

    [Header("Sound")]
    [SerializeField] private AudioSource hitSound;

    [Header("ENEMY SCRIPTABLES")]
    private EnemyScriptableDatabase scriptableDatabase;
    [SerializeField] private DeathParticleDatabase particleDatabase;

    [Header("DEATH PARTICLES")]
    [SerializeField] private ParticleSystem[] deathParticles;

    [SerializeField] private Vector3 DeathLocation;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        scriptableDatabase = EnemyScriptableDatabase.Instance;
        //deathParticles = particleDatabase.GetParticleArray;
        enemySpawn = EnemySpawn.Instance;
        moneyDrop = enemySpawn.GetComponent<MoneyDrop>();
        follower = GetComponentInParent<PathFollower>();
        _maxHealth = settings.GetHealth;
        currentHealth = _maxHealth;
    }

    public override void DamageHealth(float damage, int pierce)
    {
        hitSound.PlayOneShot(hitSound.clip, hitSound.volume);
        currentHealth -= damage;
        CheckDeath(1);
    }

    public override void CheckDeath(int pierce)
    {
        if (currentHealth <= 0)
        {
            damage = 0;
            StartCoroutine(DespawnEnemy(gameObject, .1f));

            manager.MoneyDrop(settings.GetCashDrop);

            if (parentDestroyer != null) parentDestroyer.IsChildDead = true;
            if (gameObject.GetComponent<Animator>() != null) gameObject.GetComponent<Animator>().enabled = false;


        }

    }

    public override void PreSplit(Plane[] planes)
    {
        Destroy(GetComponentInParent<PathFollower>());
        DeathLocation = transform.position;
    }

    public override void PostSplit(GameObject[] childEnemy)
    {
        foreach (GameObject g in childEnemy)
        {
            Destroy(g.GetComponentInParent<PathFollower>());
            g.AddComponent<Rigidbody>();
            Destroy(g.GetComponent<DamageEvent>());
            Destroy(g.GetComponent<ColorManager>());
            Destroy(g.GetComponent<WorldUvMapper>());
            g.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            g.AddComponent<CutManager>().LifeTime = ChildLifeTime;
            Destroy(g.GetComponent<OgreHealth>());
        }
        if (childEnemy.Length > 1)
        {
            childEnemy[0].GetComponent<Rigidbody>().AddForce(Vector3.right * 100);
            childEnemy[1].GetComponent<Rigidbody>().AddForce(Vector3.left * 100);
        }
    }

    public override IEnumerator DespawnEnemy(GameObject g, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(g);
    }

    public override IEnumerator DespawnEnemy(GameObject g)
    {
        yield return new WaitForSeconds(ChildLifeTime);
        Destroy(g);
    }

    public override bool Killed()
    {
        if (currentHealth <= 0) return true;
        else return false;
    }

    public override float GetCurrentHealth()
    {
        return currentHealth;
    }

    public override void SetCurrentHealth(float f)
    {
        currentHealth = f;
    }

    public override int GetDamageToPlayer => damage;
}
