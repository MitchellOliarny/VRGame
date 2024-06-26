using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    private GameObject spawner;
    private TargetingScript enemies;
    [SerializeField] float speed, targetSpace;
    [SerializeField] bool targetFlying = false, targetCamo = false;
    [SerializeField] private Vector3 origin;
    private GameObject targetEnemy = null;
    [SerializeField] GameObject AttackBox;
    [SerializeField] int enemy_hit_count, pierce;
    [SerializeField] float damage;
    [SerializeField] Animator anim;
    [SerializeField] AnimalAttack attack;
    private bool canAttack = true;
    void Start()
    {
        enemies = gameObject.GetComponentInParent<TargetingScript>();
        origin = gameObject.transform.position;
    }

    void OnEnable()
    {
        AttackBox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemies.GetEnemyArray.Count > 0 && targetEnemy == null)
        {
            for (int i = 0; i < enemies.GetEnemyArray.Count; i++)
            {
                if (CheckTarget(enemies.GetEnemyArray[i]))
                {
                    targetEnemy = enemies.GetEnemyArray[i];
                    break;
                }
            }

        }
        else if (targetEnemy)
        {
            if (!enemies.GetEnemyArray.Contains(targetEnemy))
            {
                targetEnemy = null;
            }
            MoveToTarget(targetEnemy);
        }
        else
        {
            Wander();
        }
    }

    private bool CheckTarget(GameObject enemy)
    {

        if (enemy.GetComponent<EnemyModifiers>().GetFlying && !targetFlying)
        {
            return false;
        }
        if (enemy.GetComponent<EnemyModifiers>().GetCamo && !targetCamo)
        {
            return false;
        }
        return true;
    }
    private void MoveToTarget(GameObject enemy)
    {
        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));
        Vector3 target = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
        transform.LookAt(target);
        if (distance > targetSpace)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), step);
        }
        else
        {
            if (canAttack)
            {
                //Start Attack Anim, attack anim will manage start of damage method
                anim.SetTrigger("attack");
                canAttack = false;
            }
        }

    }

    private void Wander()
    {
        float step = speed * Time.deltaTime;
        //Wander (not implemented)
        Vector3 target = new Vector3(origin.x, transform.position.y, origin.z);
        transform.LookAt(target);

        gameObject.transform.position = Vector3.MoveTowards(transform.position, origin, step);
    }

    public void DamageEnemy()
    {
        AttackBox.SetActive(true);
    }

    public void HitFlying() => targetFlying = true;
    public void HitCamo() => targetCamo = true;

    public int EnemyHitCount() => enemy_hit_count;
    public float AnimalDamage() => damage;
    public int AnimalPierce() => pierce;

    public void SetEnemyHitCount(int i) => enemy_hit_count = i;
    public void SetPierce(int i) => pierce = i;
    public void SetDamage(int i) => damage = i;

    public void SetCanAttackTrue()
    {
        canAttack = true;
        AttackBox.SetActive(false);
    }
}
