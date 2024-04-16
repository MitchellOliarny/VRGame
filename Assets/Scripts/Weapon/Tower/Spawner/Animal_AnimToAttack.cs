using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_AnimToAttack : MonoBehaviour
{
    private AnimalMovement a;
    private void Start()
    {
        a = gameObject.transform.parent.GetComponentInParent<AnimalMovement>();
    }
    public void Damage()
    {
        a.DamageEnemy();
    }

    public void SetAttackTrue()
    {
        a.SetCanAttackTrue();
    }
}
