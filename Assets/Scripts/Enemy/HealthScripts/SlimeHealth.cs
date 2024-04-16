using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class SlimeHealth : Health
{
    [Header("SCRIPTS")]
    [SerializeField] private EventManagerScript manager;
    [SerializeField] private DamageEvent damageEvent;
    [SerializeField] private EnemySpawn enemySpawn;
    [SerializeField] private ColorManager colorManager;
    [SerializeField] private MoneyDrop moneyDrop;
    [SerializeField] private EnemySettings settings;
    [SerializeField] private PathFollower follower;
    [SerializeField] private ParentDestroyer parentDestroyer;
    [SerializeField] private Transform enemyScale;

    [Header("HEALTH")]
    [SerializeField] private float _maxHealth; // Max health of entity
    [SerializeField] private float currentHealth; // Health holder value
    [SerializeField] private int tier;

    [SerializeField] private float ChildLifeTime;

    [Header("Sound")]
    [SerializeField] private AudioSource hitSound;

    [Header("ENEMY SCRIPTABLES")]
    private EnemyScriptableDatabase scriptableDatabase;

    [Header("DEATH PARTICLES")]
    [SerializeField] private ParticleSystem deathParticles;

    [SerializeField] private Vector3 DeathLocation;

    private bool fullDead = false;




    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        scriptableDatabase = EnemyScriptableDatabase.Instance;
        enemySpawn = EnemySpawn.Instance;
        moneyDrop = enemySpawn.GetComponent<MoneyDrop>();
        colorManager = GetComponent<ColorManager>();
        follower = GetComponentInParent<PathFollower>();
        settings = follower.settings;
        _maxHealth = settings.GetHealth;
        currentHealth = _maxHealth;
        ChildLifeTime = settings.GetChildLifeTime;

        deathParticles = settings.GetDeathParticle;
        tier = settings.GetTier;
        enemyScale.localScale = new Vector3(0.5f * settings.GetScale, 0.5f * settings.GetScale, 0.5f * settings.GetScale);

        GetComponent<MeshRenderer>().material = colorManager.GetMaterial(tier, 1);
    }

    public override void DamageHealth(float damage, int pierce)
    {
        hitSound.PlayOneShot(hitSound.clip, hitSound.volume);
        currentHealth -= damage;
        CheckDeath(pierce);
    }

    public override void CheckDeath(int pierce)
    {
        if (currentHealth <= 0)
        {
 
            manager.MoneyDrop(settings.GetCashDrop * pierce);
  
            deathParticles.Play();

            if (colorManager.DecreaseColor(tier, pierce) != null)
            {
                settings = colorManager.DecreaseColor(tier, pierce);
                tier = settings.GetTier;
                follower.SetSpeed(settings.GetSpeed);
                enemyScale.localScale = new Vector3(0.5f * settings.GetScale, 0.5f * settings.GetScale, 0.5f * settings.GetScale);
                deathParticles = settings.GetDeathParticle;
                GetComponent<MeshRenderer>().material = colorManager.GetMaterial(tier, pierce);
                currentHealth = settings.GetMaxHealth;
            }
            else
            {
                StartCoroutine(DespawnEnemy(gameObject, .1f));

                if (parentDestroyer != null)
                    parentDestroyer.IsChildDead = true;

                if (gameObject.GetComponent<Animator>() != null) gameObject.GetComponent<Animator>().enabled = false;

                fullDead = true;
            }

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

    public override int GetDamageToPlayer => tier;
}
