using UnityEngine;

public class SpinnerUpgradeManager : UpgradeMaster
{

    [Header("UPGRADES STATS PATH 1")]
    [SerializeField] private float spinSpeedUpgrade1Multiplier;
    [SerializeField] private float spinSpeedUpgrade2Multiplier;

    [Header("UPGRADES STATS PATH 2")]
    [SerializeField] private float bladeSizeUpgrade1Multiplier;
    [SerializeField] private float bladeSizeUpgrade2Multiplier;

    [Header("UPGRADES STATS PATH 3")]
    [SerializeField] private float damageUpgrade1Multiplier;
    [SerializeField] private float damageUpgrade2Multiplier;


    [Header("UPGRADES OBJECTS")]
    [SerializeField] private GameObject blade1;
    [SerializeField] private GameObject blade2;
    [SerializeField] private GameObject[] blades;

    [Header("ANIMATOR")]
    [SerializeField] private Animator anim;

    [SerializeField] private DescriptionManager manager;

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
                            anim.SetFloat("SpeedMod", spinSpeedUpgrade1Multiplier);
                            break;
                        case 2:
                            anim.SetFloat("SpeedMod", ((spinSpeedUpgrade2Multiplier + spinSpeedUpgrade1Multiplier) - 1));
                            break;
                        case 3:
                            blade1.SetActive(true);
                            break;
                        case 4:
                            blade2.SetActive(true);
                            break;
                    }

                    break;
                case 2: // MID PATH UPGRADE
                    switch (index)
                    {
                        case 1:
                            foreach (GameObject b in blades)
                                b.transform.localScale = new Vector3(bladeSizeUpgrade1Multiplier, bladeSizeUpgrade1Multiplier, bladeSizeUpgrade1Multiplier);
                            break;
                        case 2:
                            foreach (GameObject b in blades)
                                b.transform.localScale = new Vector3((bladeSizeUpgrade2Multiplier + bladeSizeUpgrade1Multiplier) - 1, (bladeSizeUpgrade2Multiplier + bladeSizeUpgrade1Multiplier) - 1, (bladeSizeUpgrade2Multiplier + bladeSizeUpgrade1Multiplier) - 1);
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
                            foreach (var b in blades)
                                b.GetComponent<Attack_Spinner>().SetDamage(damageUpgrade1Multiplier);
                            break;
                        case 2:
                            foreach (var b in blades)
                                b.GetComponent<Attack_Spinner>().SetDamage(damageUpgrade2Multiplier);
                            break;
                        case 3:

                            break;
                        case 4:

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
                if (MoneyDrop.Instance.GetCash() >= manager.GetTopPathCost(upgrade - 1))
                {
                    EventManagerScript.MoneyReduce(manager.GetTopPathCost(upgrade - 1));
                    return true;
                }
                else return false;

            case 2:
                if (MoneyDrop.Instance.GetCash() >= manager.GetMiddlePathCost(upgrade - 1))
                {
                    EventManagerScript.MoneyReduce(manager.GetMiddlePathCost(upgrade - 1));
                    return true;
                }
                else return false;
            case 3:
                if (MoneyDrop.Instance.GetCash() >= manager.GetBottomPathCost(upgrade - 1))
                {
                    EventManagerScript.MoneyReduce(manager.GetBottomPathCost(upgrade - 1));
                    return true;
                }
                else return false;
            default:
                return false;
        }

    }
}
