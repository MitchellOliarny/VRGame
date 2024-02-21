using UnityEngine;

public class SpinnerBlade : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private Collider col;
    [SerializeField] private CallSeverScript callSever;

    [SerializeField] bool sword;
    [SerializeField] int damage;
    private void Start()
    {
        ID = transform.parent.parent.parent.gameObject.GetInstanceID();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            // Checks for EnemyArrayIndex
            if (other.GetComponentInParent<EnemyArrayIndex>() != null)
            {
                // Sends Event OnBladeHit
                // Takes the Index of the enemy
                // Takes the Index of the Spinner
                if (!sword)
                {
                    EventManagerScript.BladeHit(other.GetComponentInParent<EnemyArrayIndex>().Index, ID);
                }
                else
                {
                    Debug.Log("sword hit");
                    EventManagerScript.EnemyHit(other.GetComponentInParent<EnemyArrayIndex>().Index, damage);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If Exits enemy collider
        if (other.tag == "enemy")
        {
            // If enemy has DamageEvent attatched
            if (other.gameObject.GetComponent<DamageEvent>() != null)
            {
                // If the enemy HP depleted below 0
                if (other.gameObject.GetComponent<DamageEvent>().isDead())
                    // Calls Slice Method
                    // other is the enemy collider
                    // Gets the closest point of intersection
                    callSever.CallSliceMethod(other, other.ClosestPoint(col.bounds.center));
            }
        }
    }
}
