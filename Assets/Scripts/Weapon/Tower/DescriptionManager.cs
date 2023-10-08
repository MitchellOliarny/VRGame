using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    [Header("TOWER DESCRIPTIONS")]
    [SerializeField] private string towerName;
    [SerializeField] private int buyPrice;

    [Header("UPGRADE DESCRIPTIONS PATH 1")]

    [SerializeField] private string[] path1UpgradeNames;
    [SerializeField] private string[] path1UpgradeDescrips;
    [SerializeField] private int[] path1UpgradeCosts;

    [Header("UPGRADE DESCRIPTIONS PATH 2")]

    [SerializeField] private string[] path2UpgradeNames;
    [SerializeField] private string[] path2UpgradeDescrips;
    [SerializeField] private int[] path2UpgradeCosts;

    [Header("UPGRADE DESCRIPTIONS PATH 3")]

    [SerializeField] private string[] path3UpgradeNames;
    [SerializeField] private string[] path3UpgradeDescrips;
    [SerializeField] private int[] path3UpgradeCosts;

    public string GetTowerName() => towerName;
    public string GetTopPathName(int i) => path1UpgradeNames[i];
    public string GetTopPathDescription(int i) => path1UpgradeDescrips[i];
    public int GetTopPathCost(int i) => path1UpgradeCosts[i];
    public string GetMiddlePathName(int i) => path2UpgradeNames[i];
    public string GetMiddlePathDescription(int i) => path2UpgradeDescrips[i];
    public int GetMiddlePathCost(int i) => path2UpgradeCosts[i];
    public string GetBottomPathName(int i) => path3UpgradeNames[i];
    public string GetBottomPathDescription(int i) => path3UpgradeDescrips[i];
    public int GetBottomPathCost(int i) => path3UpgradeCosts[i];

    public int GetBuyPrice => buyPrice;

}
