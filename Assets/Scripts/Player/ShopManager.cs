using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public static ShopManager Instance;

    [SerializeField] private GameObject player;

    [SerializeField] private int value;
    [SerializeField] private GameObject item;

    private void Awake()
    {
        Instance = this;
    }



    

    public void SetItem(GameObject g) => item = g;
    public void SetValue(int i) => value = i;
}
