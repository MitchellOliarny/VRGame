using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    [SerializeField] private GameObject miniTurret;
    [SerializeField] private GameObject Turret;

    [SerializeField] private Quaternion turretRotation;

    [SerializeField] private float spawnOffset;

    private int singularTurretCheck = 1;

    private void Start()
    {
        turretRotation = Turret.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            if (singularTurretCheck > 0)
            {
                Instantiate(Turret, new Vector3(miniTurret.transform.position.x, miniTurret.transform.position.y + spawnOffset, miniTurret.transform.position.z), turretRotation);
                singularTurretCheck--;
            }
            Destroy(gameObject);
        }
    }

    public void UnFreezeOnPickUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
