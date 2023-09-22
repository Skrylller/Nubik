using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private InventoryObjUI _inventoryObjUI;

    [SerializeField] private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } }

    [SerializeField] private bool _noteAutoOpen;

    private void OnEnable()
    {
        _inventoryObjUI.gameObject.SetActive(false);
    }

    public void Init()
    {
        if (_inventory == null)
            return;

        _inventoryObjUI.Init(_inventory);
        _inventoryObjUI.gameObject.SetActive(true);
    }

    public void SetInventory(Inventory items)
    {
        _inventory = items;
        _inventoryObjUI.Init(_inventory);
    }

    public void TakeItems()
    {
        for(int i = 0; i < _inventory.Items.Count; i++)
        {
            PlayerInventory.Inventory.AddItem(_inventory.Items[i].ItemModel, _inventory.Items[i].Count);
        }

        for (int i = 0; i < _inventory.KeyModels.Count; i++)
        {
            PlayerInventory.Inventory.AddItem(_inventory.KeyModels[i]);
        }

        for (int i = 0; i < _inventory.WeaponModels.Count; i++)
        {
            PlayerInventory.Inventory.AddItem(_inventory.WeaponModels[i]);
        }

        for (int i = 0; i < _inventory.NoteModels.Count; i++)
        {
            if(_noteAutoOpen)
                UIController.main.OpenNoteUI(_inventory.NoteModels[i]);

            PlayerInventory.Inventory.AddItem(_inventory.NoteModels[i]);
        }
    }
}
