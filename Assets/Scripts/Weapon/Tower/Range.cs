using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private TargetingScript targeting;

    private void Start()
    {
        targeting = gameObject.GetComponentInParent<TargetingScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            targeting.AddEnemy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            targeting.RemoveEnemy(other.gameObject);
        }
    }
}
