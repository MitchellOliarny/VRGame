using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    private GameObject spawner;
    private TargetingScript enemies;
    [SerializeField] float speed;
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
            gameObject.transform.LookAt(enemies.GetEnemyArray[0].transform.position);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, enemies.GetEnemyArray[0].transform.position, step);
        }
        else
        {
            gameObject.transform.LookAt(origin);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, origin, step);
        }
    }
}
