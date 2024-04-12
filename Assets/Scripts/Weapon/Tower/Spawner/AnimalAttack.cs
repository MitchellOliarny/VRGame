using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    public int enemy_hits;
    private int enemy_attack_count;
    private float _damage;
    private int pierce;

    void OnEnable()
    {
        //Add damage, pierce, enemy_hits into parent script and get from here
        //Parent script gets adjusted on upgrade
        enemy_hits = GetComponentInParent<AnimalMovement>().EnemyHitCount();
        _damage = GetComponentInParent<AnimalMovement>().AnimalDamage();
        pierce = GetComponentInParent<AnimalMovement>().AnimalPierce();

        enemy_attack_count = enemy_hits;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
           if (enemy_attack_count > 0)
           {
                EventManagerScript.EnemyHit(other.GetComponentInParent<EnemyArrayIndex>().Index, _damage, pierce);
                enemy_attack_count--;
           }
           else
           {
                gameObject.SetActive(false);
                enemy_attack_count = enemy_hits;
           }
        }
    }
}
