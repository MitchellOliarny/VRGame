using UnityEngine;
using System.Collections;
using PathCreation.Examples;

public class SlimeSpawn : MonoBehaviour
{
    [SerializeField] private PathFollower pathFollower;

    public void SpawnSlimeOnDeath(GameObject[] slimes, Transform deathPosition)
    {
        for (int i = 0; i < slimes.Length; i++)
        {
            GameObject temp = Instantiate(slimes[i], deathPosition);
            temp.GetComponent<PathFollower>().GetDistanceTravelled = pathFollower.GetDistanceTravelled + i;
            EnemySpawn.Instance.AddToArray(temp);
            temp.GetComponent<EnemyArrayIndex>().Index = EnemySpawn.Instance.GetEnemyArrayLength();
        }
    }
}
