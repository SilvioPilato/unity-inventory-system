using System;
using UnityEngine;

[Serializable]
public class ItemStack
{
    [SerializeField] private int quantity;
    [SerializeField] private Item item;
    [SerializeField] private int maxStackSize;

    public ItemStack(Item item = null, int quantity = 0, int maxStackSize = 1)
    {
        this.item = item;
        this.quantity = quantity;
        this.maxStackSize = maxStackSize;
    }
    
    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public Item Item
    {
        get => item;
        set => item = value;
    }

    public int MaxStackSize
    {
        get => maxStackSize;
        set => maxStackSize = value;
    }
}