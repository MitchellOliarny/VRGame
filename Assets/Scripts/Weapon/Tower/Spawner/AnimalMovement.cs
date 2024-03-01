using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    private GameObject spawner;
    private TargetingScript enemies;
    [SerializeField] float speed, targetSpace;
    [SerializeField] private Vector3 origin;
    void Start()
    {
        enemies = gameObject.GetComponentInParent<TargetingScript>();
        origin = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        if (enemies.GetEnemyArray.Count > 0)
        {
            float distance = Vector3.Distance(transform.position, enemies.GetEnemyArray[0].transform.position);
            Vector3 target = new Vector3(enemies.GetEnemyArray[0].transform.position.x, transform.position.y, enemies.GetEnemyArray[0].transform.position.z);
            transform.LookAt(target);
            if (distance > targetSpace) { gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemies.GetEnemyArray[0].transform.position.x, transform.position.y, enemies.GetEnemyArray[0].transform.position.z), step); }
        }
        else
        {
            Vector3 target = new Vector3(origin.x, transform.position.y, origin.z);
            transform.LookAt(target);

            gameObject.transform.position = Vector3.MoveTowards(transform.position, origin, step);
        }
    }
}
