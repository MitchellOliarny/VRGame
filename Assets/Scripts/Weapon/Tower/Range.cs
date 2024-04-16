using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private TargetingScript targeting;
    [SerializeField] bool targetFlying, targetCamo;

    private void Start()
    {
        targeting = gameObject.GetComponentInParent<TargetingScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            if (CheckTarget(other.gameObject)) {
                targeting.AddEnemy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            targeting.RemoveEnemy(other.gameObject);
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
}
