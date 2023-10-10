using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtonScript : MonoBehaviour
{
    [SerializeField] private int upgradeIndex;
    [SerializeField] private int upgradePath;
    [SerializeField] private ButtonMaster upgrader;
    [SerializeField] private GameObject child;
    private Button button;

    [Header("BUTTON TEXT")]
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeDescrip;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI sell;

    private float reducedSellPrice;

    private void Start()
    {
        ChangeSellPrice(upgrader.GetSellPrice);
        button = gameObject.GetComponent<Button>();
    }

    public void Upgrade()
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
        ChangeSellPrice(upgrader.GetSellPrice);
    }

    public void Sell()
    {
        upgrader.SellTower(reducedSellPrice);
    }


    public void ChildButtonState(bool state)
    {
        child.SetActive(state);
        button.interactable = state;
    }

    public void SetUpgradeName(string i) => upgradeName.text = $"{i}";
    public void SetUpgradeDescrip(string i) => upgradeDescrip.text = $"{i}";
    public void SetUpgradeCost(int i) => upgradeCost.text = "Cost: " + i;

    public void ChangeSellPrice(int i) { reducedSellPrice = i * 0.7f; SetSellPrice(reducedSellPrice); }
    private void SetSellPrice(float i) => sell.text = "Sell: " + i;
}
