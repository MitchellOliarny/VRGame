using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerUpgrades : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] TurretShoot shooting;

    [SerializeField] bool[] path1, path2, path3;
    [SerializeField] ScriptableObject[] path1Upgrades, path2Upgrades, path3Upgrades;
 
    void Start()
    {
        tower = this.gameObject;
        //We want to modify this current instance of the Tower, not the scriptable that will modify future towers
        shooting = tower.GetComponent<TurretShoot>();
    }

    void UpgradeTower(ScriptableObject Upgrade)
    {

    }

    public void UpgradePath1(int slot)
    {
        UpgradeTower(path1Upgrades[slot]);
        path1[slot] = true;
    }
    public void UpgradePath2(int slot)
    {
        UpgradeTower(path2Upgrades[slot]);
        path2[slot] = true;
    }
    public void UpgradePath3(int slot)
    {
        UpgradeTower(path3Upgrades[slot]);
        path3[slot] = true;
    }
}
