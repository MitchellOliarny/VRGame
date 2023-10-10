using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    [SerializeField] private GameObject miniTurret;
    [SerializeField] private GameObject Turret;

    [SerializeField] public int cost;
    [SerializeField] private EventManagerScript manager;


    [SerializeField] private Quaternion turretRotation;

    [SerializeField] private float spawnOffset;


    private int singularTurretCheck = 1;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        turretRotation = Turret.transform.rotation;
    }

    private void OnTransformParentChanged()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        UnFreezeOnPickUp();
        if(manager.GetMoney < cost)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ground") && gameObject.GetComponent<Rigidbody>().useGravity == true)
        {
            if (singularTurretCheck > 0)
            {
                Instantiate(Turret, new Vector3(miniTurret.transform.position.x, miniTurret.transform.position.y + spawnOffset, miniTurret.transform.position.z), turretRotation);
                singularTurretCheck--;
                manager.MoneyReduce(cost);
            }
            Destroy(gameObject);
        }
    }

    public void UnFreezeOnPickUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
