using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class EndOfPath : MonoBehaviour
{
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private PathCreator path;

    [SerializeField] private float maxPathDistance;

    private void Start()
    {
        path = GameObject.FindGameObjectWithTag("path").GetComponent<PathCreator>();
        maxPathDistance = path.path.length;
    }

    private void FixedUpdate()
    {
        if (pathFollower.GetDistanceTravelled >= maxPathDistance)
        {
            Debug.Log("REACHED END");

            EventManagerScript.EnemyReachedEnd(GetComponentInChildren<Health>().GetDamageToPlayer);

            EnemySpawn.Instance.RemoveFromArray(gameObject);
            Destroy(gameObject);
        }
    }
}
