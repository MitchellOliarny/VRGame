using UnityEngine;
using TMPro;

public class UpgradeButtonScript : MonoBehaviour
{
    [SerializeField] private int upgradeIndex;
    [SerializeField] private int upgradePath;
    [SerializeField] private ButtonMaster upgrader;
    [SerializeField] private GameObject child;

    [Header("BUTTON TEXT")]
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeDescrip;
    [SerializeField] private TextMeshProUGUI upgradeCost;


    private void Upgrade()
    {
        switch (upgradePath)
        {
            case 1:
                upgrader.UpgradePath1(upgradeIndex);
                break;
            case 2:
                upgrader.UpgradePath2(upgradeIndex);
                break;
            case 3:
                upgrader.UpgradePath3(upgradeIndex);
                break;
        }
    }


    public void ChildButtonState(bool state)
    {
        child.SetActive(state);
    }

    public void SetUpgradeName(string i) => upgradeName.text = $"{i}";
    public void SetUpgradeDescrip(string i) => upgradeDescrip.text = $"{i}";
    public void SetUpgradeCost(int i) => upgradeCost.text = "Cost: " + i;
}
