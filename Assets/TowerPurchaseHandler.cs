using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPurchaseHandler : MonoBehaviour
{

    [SerializeField] private GameObject[] towers;
    [SerializeField] private int currTower = 0;

    [SerializeField] private GameObject towerAttach;
    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private GameObject left;

    [SerializeField] private GameObject right;


    // Start is called before the first frame update
    void Start()
    {
        GameObject tower = towers[0];
        GameObject spawnedTower = GameObject.Instantiate(tower, new Vector3(0f, 0f, .05f), Quaternion.Euler(0f, 180f, 0f), towerAttach.transform);
        spawnedTower.transform.localScale = new Vector3(.1f, .1f, .05f);
        spawnedTower.GetComponent<Rigidbody>().useGravity = false;
        spawnedTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        spawnedTower.transform.position = towerAttach.transform.position;
        spawnedTower.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        costText.text = "Cost: " + spawnedTower.GetComponent<TurretSpawn>().cost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TowerLeft()
    {
        if (currTower - 1 < 0)
        {
            currTower = towers.Length - 1;
        }
        else
        {
            currTower -= 1;
        }

        if (towerAttach.transform.childCount > 0)
        {
            Destroy(towerAttach.transform.GetChild(0).gameObject);
        }
        GameObject tower = towers[currTower];
        GameObject spawnedTower = GameObject.Instantiate(tower, new Vector3(0f, 0f, .05f), Quaternion.Euler(0f, 180f, 0f), towerAttach.transform);
        spawnedTower.transform.localScale = new Vector3(.1f, .1f, .05f);
        spawnedTower.GetComponent<Rigidbody>().useGravity = false;
        spawnedTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        spawnedTower.transform.position = towerAttach.transform.position;
        spawnedTower.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        costText.text = "Cost: " + spawnedTower.GetComponent<TurretSpawn>().cost;
    }

    public void TowerRight() 
    {
        if (currTower + 1 > towers.Length)
        {
            currTower = 0;
        }
        else
        {
            currTower += 1;
        }

        if (towerAttach.transform.childCount > 0)
        {
            Destroy(towerAttach.transform.GetChild(0).gameObject);
        }

        GameObject tower = towers[currTower];
        GameObject spawnedTower = GameObject.Instantiate(tower, new Vector3(0f, 0f, .05f), Quaternion.Euler(0f, 180f, 0f), towerAttach.transform);
        spawnedTower.transform.localScale = new Vector3(.1f, .1f, .05f);
        spawnedTower.GetComponent<Rigidbody>().useGravity = false;
        spawnedTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        spawnedTower.transform.position = towerAttach.transform.position;
        spawnedTower.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        costText.text = "Cost: " + spawnedTower.GetComponent<TurretSpawn>().cost;
    }
}
