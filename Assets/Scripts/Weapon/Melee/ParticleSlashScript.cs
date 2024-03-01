using UnityEngine;

public class ParticleSlashScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] int pierce;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (other.GetComponent<DamageEvent>() != null)
            {
                DamageEvent temp = other.GetComponent<DamageEvent>();

                if (other.GetComponentInParent<EnemyArrayIndex>() != null)
                    EventManagerScript.EnemyHit(other.GetComponentInParent<EnemyArrayIndex>().Index, damage, pierce);

                if (temp.isDead())
                {
                    CallShatterEvent(other, transform.position);
                }
            }
        }
    }

    private void CallShatterEvent(Collider other, Vector3 hitPoint)
    {
        other.SendMessage("Shatter", hitPoint, SendMessageOptions.DontRequireReceiver);
    }
}
