using UnityEngine;

public class SpawnerUpgradeManager : UpgradeMaster
{
    [SerializeField] private EventManagerScript eventManager;

    [Header("UPGRADES STATS PATH 1")]
    [SerializeField] private GameObject[] bats;
    [SerializeField] private int batsSpawn1, batsSpawn2, batsSpawn3, batsSpawn4;
    [SerializeField] private float Range3;
    [SerializeField] private int batLayerBreak4;
    [SerializeField] private bool camo = false;

    [Header("UPGRADES STATS PATH 2")]
    [SerializeField] private GameObject[] snakes;
    [SerializeField] private int snakesSpawn1, snakesSpawn3;
    [SerializeField] private int snakeLayerBreak2;
    [SerializeField] private bool acid = false;

    [Header("UPGRADES STATS PATH 3")]
    [SerializeField] private GameObject[] wolves;
    [SerializeField] private int wolvesSpawn1, wolvesSpawn4;
    [SerializeField] private float wolfDMGIncrease;
    [SerializeField] private int wolfAttackEnemyCount;


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
                            for (int i = 0; i < batsSpawn1; i++)
                            {
                                bats[i].SetActive(true);
                            }
                            break;
                        case 2:
                            for (int i = batsSpawn1; i < batsSpawn2 + batsSpawn1; i++)
                            {
                                bats[i].SetActive(true);
                            }
                            break;
                        case 3:
                            for (int i = batsSpawn2 + batsSpawn1; i < batsSpawn2 + batsSpawn1 + batsSpawn3; i++)
                            {
                                bats[i].SetActive(true);
                            }
                            range.localScale = new Vector3(range.localScale.x * Range3, 0.01f, range.localScale.z * Range3);
                            break;
                        case 4:
                            for (int i = batsSpawn3 + batsSpawn2 + batsSpawn1; i < batsSpawn2 + batsSpawn1 + batsSpawn3 + batsSpawn4; i++)
                            {
                                bats[i].SetActive(true);
                            }
                            break;
                    }

                    break;
                case 2: // MID PATH UPGRADE
                    switch (index)
                    {
                        case 1:
                            for (int i = 0; i < snakesSpawn1; i++)
                            {
                                snakes[i].SetActive(true);
                            }
                            break;
                        case 2:
                            //Snake Layer Break 2
                            break;
                        case 3:
                            for (int i = snakesSpawn1; i < snakesSpawn1 + snakesSpawn3; i++)
                            {
                                snakes[i].SetActive(true);
                            }
                            break;
                        case 4:
                            //Acid Pool
                            break;
                    }

                    break;
                case 3: // BOTTOM PATH UPGRADE

                    switch (index)
                    {
                        case 1:
                            for (int i = 0; i < wolvesSpawn1; i++)
                            {
                                wolves[i].SetActive(true);
                            }
                            break;
                        case 2:
                            //Wolves damage *= upgrade
                            break;
                        case 3:
                            //Wolves attacks hit x targets
                            break;
                        case 4:
                            for (int i = wolvesSpawn1; i < wolvesSpawn1 + wolvesSpawn4; i++)
                            {
                                wolves[i].SetActive(true);
                            }
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
