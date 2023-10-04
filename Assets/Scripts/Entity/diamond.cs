using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamond : MonoBehaviour
{
    [SerializeField] private LifeController _life;
    [SerializeField] private ItemModel _item;
    [SerializeField] private uint count = 1;

    private void Start()
    {
        _life.OnDeath += GetDiamond;
    }

    private void GetDiamond()
    {
        PlayerInventory.Inventory.AddItem(_item, count);
    }
}
