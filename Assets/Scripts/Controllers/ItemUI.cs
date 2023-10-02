using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private ItemModel item;
    [SerializeField] private ItemPrefab itemPrefab;

    private void Start()
    {
        itemPrefab.InitWithCounter(PlayerInventory.Inventory.GetInventoryItem(item.Item));
    }
}
