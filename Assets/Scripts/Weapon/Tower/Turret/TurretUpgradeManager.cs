using UnityEngine;

public class TurretUpgradeManager : UpgradeMaster
{


    [Header("UPGRADES STATS PATH 1")]
    [SerializeField] private float damageUpgrade1Multiplier;
    [SerializeField] private float damageUpgrade2Multiplier;

    [Header("UPGRADES STATS PATH 2")]
    [SerializeField] private float firerateUpgrade1Multiplier;
    [SerializeField] private float firerateUpgrade2Multiplier;

    [Header("UPGRADES STATS PATH 3")]


    [Header("UPGRADES OBJECTS")]


    [Header("ANIMATOR")]
    [SerializeField] private Animator anim;


    public override bool Upgrade(int path, int index)
    {
        return false;
        switch (path)
        {
            case 1: // TOP PATH UPGRADE

                switch (index)
                {
                    case 1:
                        this.GetComponent<TurretShoot>().SetDamage(damageUpgrade1Multiplier);
                        break;
                    case 2:
                        this.GetComponent<TurretShoot>().SetDamage(damageUpgrade2Multiplier);
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        
                        break;
                }

                break;
            case 2: // MID PATH UPGRADE
                switch (index)
                {
                    case 1:
                        this.GetComponent<TurretShoot>().SetFireRate(firerateUpgrade1Multiplier);
                        break;
                    case 2:
                        this.GetComponent<TurretShoot>().SetFireRate(firerateUpgrade2Multiplier);
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                }

                break;
            case 3: // BOTTOM PATH UPGRADE

                switch (index)
                {
                    case 1:
                        
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                }
                break;
        }
    }
}
