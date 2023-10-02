using UnityEngine;
using TMPro;

public class MoneyDrop : MonoBehaviour
{
    [Header("SCRIPTS")]
    public static MoneyDrop Instance;
    [SerializeField] private UISettings settings;

    [Header("INTS")]
    [SerializeField] private int cash;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        Instance = this;
    }



    private void Start()
    {
        moneyText.text = cash.ToString();
    }


    public int GetCash() => cash;
}
