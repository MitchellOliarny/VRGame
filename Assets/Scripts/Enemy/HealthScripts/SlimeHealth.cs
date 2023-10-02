using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class SlimeHealth : Health
{
    [Header("SCRIPTS")]
    [SerializeField] private DamageEvent damageEvent;
    [SerializeField] private EnemySpawn enemySpawn;
    [SerializeField] private ColorManager colorManager;
    [SerializeField] private MoneyDrop moneyDrop;
    [SerializeField] private EnemySettings settings;
    [SerializeField] private PathFollower follower;
    [SerializeField] private ParentDestroyer parentDestroyer;

    [Header("HEALTH")]
    [SerializeField] private float _maxHealth; // Max health of entity
    [SerializeField] private float currentHealth; // Health holder value
    [SerializeField] private int damage;
    [SerializeField] private int tier;

    [SerializeField] private float ChildLifeTime;

    [Header("Sound")]
    [SerializeField] private AudioSource hitSound;

    [Header("ENEMY SCRIPTABLES")]
    private EnemyScriptableDatabase scriptableDatabase;
    [SerializeField] private DeathParticleDatabase particleDatabase;

    [Header("DEATH PARTICLES")]
    [SerializeField] private ParticleSystem[] deathParticles;

    [SerializeField] private Vector3 DeathLocation;

    private bool fullDead = false;




    // Start is called before the first frame update
    void Start()
    {

        scriptableDatabase = EnemyScriptableDatabase.Instance;
        deathParticles = particleDatabase.GetParticleArray;
        enemySpawn = EnemySpawn.Instance;
        moneyDrop = enemySpawn.GetComponent<MoneyDrop>();
        colorManager = GetComponent<ColorManager>();
        follower = GetComponentInParent<PathFollower>();
        settings = follower.settings;
        _maxHealth = settings.GetHealth;
        currentHealth = _maxHealth;
        ChildLifeTime = settings.GetChildLifeTime;

        tier = settings.GetTier;
        GetComponentInParent<Transform>().localScale = new Vector3(0.5f * settings.GetScale, 0.5f * settings.GetScale, 0.5f * settings.GetScale);

        GetComponent<MeshRenderer>().material = colorManager.GetMaterial(tier);
    }

    public override void DamageHealth(float damage)
    {
        hitSound.PlayOneShot(hitSound.clip, hitSound.volume);
        currentHealth -= damage;
        CheckDeath();
    }

    public override void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            EventManagerScript.MoneyDrop(settings.GetCashDrop(0));

            if (colorManager.DecreaseColor(tier) != null)
            {
                Debug.Log(colorManager.DecreaseColor(tier));
                settings = colorManager.DecreaseColor(tier);
                tier = settings.GetTier;
                follower.UpdateSpeed(settings.GetSpeed);
                GetComponentInParent<Transform>().localScale = new Vector3(0.5f * settings.GetScale, 0.5f * settings.GetScale, 0.5f * settings.GetScale);
                GetComponent<MeshRenderer>().material = colorManager.GetMaterial(tier);
                currentHealth = 1;
            }
            else
            {
                StartCoroutine(DespawnEnemy(gameObject, .1f));

                if (parentDestroyer != null)
                    parentDestroyer.IsChildDead = true;

                if (gameObject.GetComponent<Animator>() != null) gameObject.GetComponent<Animator>().enabled = false;

                fullDead = true;
            }
            //TODO: Create Death Effect

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
            g.GetComponent<MeshCollider>().isTrigger = false;
            g.AddComponent<Rigidbody>();
            Destroy(g.GetComponent<DamageEvent>());
            Destroy(g.GetComponent<ColorManager>());
            g.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            g.AddComponent<CutManager>().LifeTime = ChildLifeTime;
            Destroy(g.GetComponent<SlimeHealth>());
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
        if (fullDead) return true;
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
