using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ButtonMaster : MonoBehaviour
{
    private DescriptionManager descriptions;
    private UpgradeMaster upgrader;
    private EventManagerScript manager;

    [Header("MAX UPGRADES")]
    [SerializeField] private int MAXUPGRADES = 7;
    [SerializeField] private int currentUpgrades;
    [SerializeField] private int sellPrice = 0;

    [Header("BUTTONS")]
    [SerializeField] private GameObject[] upgradeBtn1, upgradeBtn2, upgradeBtn3;
    [SerializeField] private TextMeshProUGUI upgradesText;

    private int currentTop = 0, currentMiddle = 0, currentBottom = 0;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManagerScript>();
        upgradesText.text = "0 / " + MAXUPGRADES;
        if (GetComponentInParent<UpgradeMaster>() != null)
        {
            upgrader = GetComponentInParent<UpgradeMaster>();
        }
        SetDescriptions();
        sellPrice += descriptions.GetBuyPrice;
    }

    private void Update()
    {
            if (manager.GetMoney < descriptions.GetTopPathCost(currentTop))
            {
                upgradeBtn1[currentTop].GetComponent<Image>().color = new Color32(207, 24, 24, 255);
            }
            else
            {
                upgradeBtn1[currentTop].GetComponent<Image>().color = new Color32(0, 181, 255, 255);
            }

            if (manager.GetMoney < descriptions.GetMiddlePathCost(currentMiddle))
            {
                upgradeBtn2[currentMiddle].GetComponent<Image>().color = new Color32(207, 24, 24, 255);
            }
            else
            {
                upgradeBtn2[currentMiddle].GetComponent<Image>().color = new Color32(0, 181, 255, 255);
            }



        if (manager.GetMoney < descriptions.GetBottomPathCost(currentBottom))
        {
            upgradeBtn3[currentBottom].GetComponent<Image>().color = new Color32(207, 24, 24, 255);
        }
        else
        {
            upgradeBtn3[currentBottom].GetComponent<Image>().color = new Color32(0, 181, 255, 255);
        }
    }

    public void UpgradePath1(int upgrade)
    {
        if (currentUpgrades >= MAXUPGRADES)
        {
            DisableButtons();
            return;
        }

        if (upgrader.Upgrade(1, upgrade))
        {
            currentUpgrades++;
            currentTop += 1;
            sellPrice += descriptions.GetTopPathCost(upgrade - 1);
            ButtonDeactivator(upgradeBtn1[upgrade - 1]);

            if (upgrade < upgradeBtn1.Length)
                StartCoroutine(buttonActivator(upgradeBtn1[upgrade]));
        }
    }
    public void UpgradePath2(int upgrade)
    {
        if (currentUpgrades >= MAXUPGRADES)
        {
            DisableButtons();
            return;
        }

        if (upgrader.Upgrade(2, upgrade))
        {
            currentUpgrades++;
            currentMiddle += 1;
            sellPrice += descriptions.GetMiddlePathCost(upgrade - 1);
            ButtonDeactivator(upgradeBtn2[upgrade - 1]);

            if (upgrade < upgradeBtn2.Length)
                StartCoroutine(buttonActivator(upgradeBtn2[upgrade]));
        }
    }
    public void UpgradePath3(int upgrade)
    {
        if (currentUpgrades >= MAXUPGRADES)
        {
            DisableButtons();
            return;
        }

        if (upgrader.Upgrade(3, upgrade))
        {
            currentUpgrades++;
            currentBottom += 1;
            sellPrice += descriptions.GetBottomPathCost(upgrade - 1);
            ButtonDeactivator(upgradeBtn3[upgrade - 1]);

            if (upgrade < upgradeBtn3.Length)
                StartCoroutine(buttonActivator(upgradeBtn3[upgrade]));
        }
    }

    private void DisableButtons()
    {
        foreach (GameObject g in upgradeBtn1)
        {
            g.transform.localScale = new Vector3(1f, 1f, 1f);
            g.GetComponent<UpgradeButtonScript>().ChildButtonState(false);
        }
        foreach (GameObject g in upgradeBtn2)
        {
            g.transform.localScale = new Vector3(1f, 1f, 1f);
            g.GetComponent<UpgradeButtonScript>().ChildButtonState(false);
        }
        foreach (GameObject g in upgradeBtn3)
        {
            g.transform.localScale = new Vector3(1f, 1f, 1f);
            g.GetComponent<UpgradeButtonScript>().ChildButtonState(false);
        }
    }

    private IEnumerator buttonActivator(GameObject button)
    {
        yield return new WaitForSeconds(.1f);
        button.transform.localScale = new Vector3(2f, 2f, 2f);
        button.GetComponent<UpgradeButtonScript>().ChildButtonState(true);
    }

    private void ButtonDeactivator(GameObject button)
    {
        upgradesText.text = currentUpgrades + " / " + MAXUPGRADES;
        button.transform.localScale = new Vector3(1f, 1f, 1f);
        button.GetComponent<Image>().color = new Color32(0, 248, 70, 255);
        button.GetComponent<UpgradeButtonScript>().ChildButtonState(false);
    }

    #region Getters
    public GameObject[] GetButtonsPath1() => upgradeBtn1;
    public GameObject[] GetButtonsPath2() => upgradeBtn2;
    public GameObject[] GetButtonsPath3() => upgradeBtn3;
    public int GetSellPrice => sellPrice;

    public void SellTower(float i) { Destroy(descriptions.gameObject); manager.MoneyDrop(i); }

    #endregion

    #region SetDescriptions
    private void SetDescriptions()
    {
        if (GetComponentInParent<DescriptionManager>() != null)
        {
            descriptions = GetComponentInParent<DescriptionManager>();
            string tower = descriptions.GetTowerName();
            if (descriptions.GetMiddlePathName(3) == "" && tower != "")
            {
                for (int i = 0; i < upgradeBtn1.Length; i++)
                {
                    UpgradeButtonScript temp = upgradeBtn1[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp2 = upgradeBtn2[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp3 = upgradeBtn3[i].GetComponent<UpgradeButtonScript>();
                    temp.SetUpgradeName($"{tower}_top_path_upgrade_{i + 1}_name");
                    temp.SetUpgradeDescrip($"{tower}_top_path_upgrade_{i + 1}_description");
                    temp.SetUpgradeCost(0);
                    temp2.SetUpgradeName($"{tower}_middle_path_upgrade_{i + 1}_name");
                    temp2.SetUpgradeDescrip($"{tower}_middle_path_upgrade_{i + 1}_description");
                    temp2.SetUpgradeCost(0);
                    temp3.SetUpgradeName($"{tower}_bottom_path_upgrade_{i + 1}_name");
                    temp3.SetUpgradeDescrip($"{tower}_bottom_path_upgrade_{i + 1}_description");
                    temp3.SetUpgradeCost(0);
                }
            }
            else if (descriptions.GetMiddlePathName(3) != "" && tower != "")
            {
                for (int i = 0; i < upgradeBtn1.Length; i++)
                {
                    UpgradeButtonScript temp = upgradeBtn1[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp2 = upgradeBtn2[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp3 = upgradeBtn3[i].GetComponent<UpgradeButtonScript>();
                    temp.SetUpgradeName(descriptions.GetTopPathName(i));
                    temp.SetUpgradeDescrip(descriptions.GetTopPathDescription(i));
                    temp.SetUpgradeCost(descriptions.GetTopPathCost(i));
                    temp2.SetUpgradeName(descriptions.GetMiddlePathName(i));
                    temp2.SetUpgradeDescrip(descriptions.GetMiddlePathDescription(i));
                    temp2.SetUpgradeCost(descriptions.GetMiddlePathCost(i));
                    temp3.SetUpgradeName(descriptions.GetBottomPathName(i));
                    temp3.SetUpgradeDescrip(descriptions.GetBottomPathDescription(i));
                    temp3.SetUpgradeCost(descriptions.GetBottomPathCost(i));
                }
            }
            else
            {
                for (int i = 0; i < upgradeBtn1.Length; i++)
                {
                    UpgradeButtonScript temp = upgradeBtn1[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp2 = upgradeBtn2[i].GetComponent<UpgradeButtonScript>();
                    UpgradeButtonScript temp3 = upgradeBtn3[i].GetComponent<UpgradeButtonScript>();
                    temp.SetUpgradeName("towername_top_path_upgrade_" + (i + 1) + "_name");
                    temp.SetUpgradeDescrip("towername_top_path_upgrade_" + (i + 1) + "_description");
                    temp.SetUpgradeCost(0);
                    temp2.SetUpgradeName("towername_middle_path_upgrade_" + (i + 1) + "_name");
                    temp2.SetUpgradeDescrip("towername_middle_path_upgrade_" + (i + 1) + "_description");
                    temp2.SetUpgradeCost(0);
                    temp3.SetUpgradeName("towername_bottom_path_upgrade_" + (i + 1) + "_name");
                    temp3.SetUpgradeDescrip("towername_bottom_path_upgrade_" + (i + 1) + "_description");
                    temp3.SetUpgradeCost(0);
                }
            }
        }
        else
        {
            for (int i = 0; i < upgradeBtn1.Length; i++)
            {
                UpgradeButtonScript temp = upgradeBtn1[i].GetComponent<UpgradeButtonScript>();
                UpgradeButtonScript temp2 = upgradeBtn2[i].GetComponent<UpgradeButtonScript>();
                UpgradeButtonScript temp3 = upgradeBtn3[i].GetComponent<UpgradeButtonScript>();
                temp.SetUpgradeName("Null");
                temp.SetUpgradeDescrip("No Description Manager");
                temp.SetUpgradeCost(0);
                temp2.SetUpgradeName("Null");
                temp2.SetUpgradeDescrip("No Description Manager");
                temp2.SetUpgradeCost(0);
                temp3.SetUpgradeName("Null");
                temp3.SetUpgradeDescrip("No Description Manager");
                temp3.SetUpgradeCost(0);
            }
        }
    }
    #endregion
}