using UnityEngine;

public class CrossbowUpgradeManager : UpgradeMaster
{
    [SerializeField] private EventManagerScript eventManager;

    [Header("UPGRADES STATS PATH 1")]
    [SerializeField] private int pierceUpgrade1;
    [SerializeField] private int pierceUpgrade2;
    [SerializeField] private int pierceUpgrade3;
    

    [Header("UPGRADES STATS PATH 2")]
    [SerializeField] private float damageUpgrade1Multiplier, damageUpgrade2Multiplier, damageUpgrade3Multiplier, damageUpgrade4Multiplier;

    [Header("UPGRADES STATS PATH 3")]
    [SerializeField] private float rangeUpgrade1, rangeUpgrade2, rangeUpgrade3;
    [SerializeField] private bool increaseRangeDamage;


    [Header("UPGRADES OBJECTS")]
    [SerializeField] private TurretShoot _turretShoot;
    [SerializeField] private Transform range;

    [Header("ANIMATOR")]
    [SerializeField] private Animator anim;

    [SerializeField] private DescriptionManager manager;

    private void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
    }

    public override bool Upgrade(int path, int index)
    {
        if (CanBuy(path, index))
        {
            switch (path)
            {
                case 1: // TOP PATH UPGRADE

                    switch (index)
                    {
                        case 1:
                            _turretShoot.SetPierce(pierceUpgrade1);
                            break;
                        case 2:
                            _turretShoot.SetPierce(pierceUpgrade2);
                            break;
                        case 3:
                            _turretShoot.SetRichochet(true);
                            break;
                        case 4:
                            _turretShoot.SetPierce(pierceUpgrade3);
                            break;
                    }

                    break;
                case 2: // MID PATH UPGRADE
                    switch (index)
                    {
                        case 1:
                            this.GetComponent<TurretShoot>().SetDamage(damageUpgrade1Multiplier);
                            break;
                        case 2:
                            this.GetComponent<TurretShoot>().SetDamage(damageUpgrade2Multiplier);
                            break;
                        case 3:
                            this.GetComponent<TurretShoot>().SetDamage(damageUpgrade3Multiplier);
                            break;
                        case 4:
                            this.GetComponent<TurretShoot>().SetDamage(damageUpgrade4Multiplier);
                            break;
                    }

                    break;
                case 3: // BOTTOM PATH UPGRADE

                    switch (index)
                    {
                        case 1:
                            range.localScale = new Vector3(range.localScale.x * rangeUpgrade1, 0.01f, range.localScale.z * rangeUpgrade1);
                            break;
                        case 2:
                            range.localScale = new Vector3(range.localScale.x * rangeUpgrade2, 0.01f, range.localScale.z * rangeUpgrade2);
                            break;
                        case 3:
                            _turretShoot.SetCamoDetect(true);
                            break;
                        case 4:
                            range.localScale = new Vector3(range.localScale.x * rangeUpgrade3, 0.01f, range.localScale.z * rangeUpgrade3);
                            break;
                    }
                    break;
            }
            return true;
        }
        else return false;

        
    }

    private bool CanBuy(int path, int upgrade)
    {
        switch (path)
        {
            case 1:
                if (eventManager.GetMoney >= manager.GetTopPathCost(upgrade - 1))
                {
                    eventManager.MoneyReduce(manager.GetTopPathCost(upgrade - 1));
                    return true;
                }
                else return false;

            case 2:
                if (eventManager.GetMoney >= manager.GetMiddlePathCost(upgrade - 1))
                {
                    eventManager.MoneyReduce(manager.GetMiddlePathCost(upgrade - 1));
                    return true;
                }
                else return false;
            case 3:
                if (eventManager.GetMoney >= manager.GetBottomPathCost(upgrade - 1))
                {
                    eventManager.MoneyReduce(manager.GetBottomPathCost(upgrade - 1));
                    return true;
                }
                else return false;
            default:
                return false;
        }

    }
}
