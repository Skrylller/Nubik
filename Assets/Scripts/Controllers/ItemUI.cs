using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private ItemModel item;
    [SerializeField] private ItemPrefab itemPrefab;

    private void Start()
    {
        if (PlayerInventory.Inventory.GetInventoryItem(item.Item) == null)
        {
            PlayerInventory.Inventory.AddItem(item, 5);
        }

        itemPrefab.InitWithCounter(PlayerInventory.Inventory.GetInventoryItem(item.Item));
    }
}
