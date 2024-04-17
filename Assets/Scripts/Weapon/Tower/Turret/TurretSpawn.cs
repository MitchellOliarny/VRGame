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
    [SerializeField] private bool isPlayerWeapon = false;

    private int singularTurretCheck = 1;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        turretRotation = Turret.transform.rotation;
    }

    private void OnTransformParentChanged()
    {
        if (manager.GetMoney < cost)
        {
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            if (isPlayerWeapon)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            UnFreezeOnPickUp();
            if (isPlayerWeapon)
            {
                if(transform.parent == null)
                {
                    Destroy(gameObject);
                    GameObject weapon = Instantiate(Turret, new Vector3(miniTurret.transform.position.x, miniTurret.transform.position.y + spawnOffset, miniTurret.transform.position.z), turretRotation);
                    manager.MoneyReduce(cost);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ground") && gameObject.GetComponent<Rigidbody>().useGravity == true && !isPlayerWeapon)
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
