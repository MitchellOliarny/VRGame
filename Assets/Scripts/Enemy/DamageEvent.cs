using UnityEngine;
public class DamageEvent : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private SlimeHealth health; // Health Script
    [SerializeField] private EnemyModifiers mod; // Enemy Modifier Script
    [SerializeField] private EnemyArrayIndex indexer;

    private void OnEnable()
    {
        EventManagerScript.OnEnemyHit += DamageEnemy;
    }

    private void OnDisable()
    {
        EventManagerScript.OnEnemyHit -= DamageEnemy;
    }

    private void Start()
    {
        mod = GetComponent<EnemyModifiers>(); // Set Mod to enemy Modifier
        indexer = GetComponentInParent<EnemyArrayIndex>();
        health = GetComponent<SlimeHealth>();
    }

    //-- DAMAGE ENEMY --\\
    // FLOAT: Incoming Damage
    public void DamageEnemy(int index, float damage)
    {
        if (index == indexer.Index)
        {
            Debug.Log("raw DMG " + damage);
            // If enemy is reinforced multiply damage by .5f
            if (mod.GetReinforced) { damage *= .5f; Debug.Log("new DMG " + damage); }

            // Damage enemy
            health.DamageHealth(damage);
        }
    }
    //-- GET ENEMY MODIFIERS --\\
    // Returns Modifiers of enemy
    public EnemyModifiers GetEnemyModifiers => mod;

    //-- IS DEAD --\\
    // Returns if enemy is dead
    public bool isDead() => health.Killed();
}
