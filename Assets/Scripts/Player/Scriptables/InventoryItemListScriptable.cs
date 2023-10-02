using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class InventoryItemListScriptable : ScriptableObject
{
    [SerializeField] private List<GameObject> ItemList;

    public List<GameObject> GetItemList => ItemList;

    public GameObject GetItem(int i) => ItemList[i];

    public int GetItemIndex(GameObject g)
    {
        if (ItemList.Contains(g)) return ItemList.IndexOf(g);
        else
        {
            Debug.LogError("ITEM LIST DID NOT CONTAIN " + g.name + " \n RETURNING -1");
            return -1;
        }
            
            
    }
}
