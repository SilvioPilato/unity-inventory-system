using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Container")]
public class InventoryContainer: ScriptableObject
{
    private const int SlotIndexNone = -1;
    [SerializeField] 
    private int size;
    [SerializeField]
    private ItemStack[] container;
    public ItemStack[] Container => container;
    public int Size => size;

    private void Awake()
    {
        container = new ItemStack[Size];
    }

    public int AddNew(ItemStack itemStack)
    {
        if (IsInventoryFull()) return SlotIndexNone;
        var index = GETFirstEmptySlotIndex();
        Container[index] = itemStack;
        return index;
    }

    public void AddAt(int index, ItemStack itemStack)
    {
        if (IsInventoryFull()) return;
        if (container[index].Item != null) return;

        container[index] = itemStack;
    }

    private int GETFirstEmptySlotIndex()
    {
        var firstEmpty = Container.First(itemStack => itemStack.Item == null);
        return Array.IndexOf(container, firstEmpty);
    }

    private bool IsInventoryFull()
    {
        return GETFirstEmptySlotIndex() == SlotIndexNone;
    }
    
    public int Stash(ItemStack itemStack)
    {
        var availableStacks = Array.FindAll(
            Container,
            stack => IsStackAvailable(stack, itemStack)
        );
        var remainingQuantity = PileOn(availableStacks, itemStack);
        if (remainingQuantity <= 0) return 0;
        itemStack.Quantity = remainingQuantity;
        var index = AddNew(itemStack);
        return index == SlotIndexNone ? remainingQuantity : 0;
    }

    [CanBeNull]
    public ItemStack Remove(int itemIndex, int amount = 0)
    {
        if (itemIndex >= Container.Length) return null;
        
        var stack = Container[itemIndex];
        if (amount == 0 || amount >= stack.Quantity )
        {
            Container[itemIndex] = new ItemStack();
            return stack;
        }
        Container[itemIndex].Quantity -= amount;
        return new ItemStack(stack.Item) {MaxStackSize = stack.MaxStackSize, Quantity = amount};
    }
    
    private static bool IsStackAvailable(ItemStack stackToFill, ItemStack stackToAdd)
    {
        return stackToFill.Item.Equals(stackToAdd.Item)
               && stackToFill.Quantity < stackToFill.MaxStackSize;
    }
    
    private int PileOn(IEnumerable<ItemStack> availableStacks, ItemStack stackToPile)
    {
        var remaining = stackToPile.Quantity;
        foreach (var stack in availableStacks)
        {
            var spaceAvailable = stack.MaxStackSize - stack.Quantity;
            if (spaceAvailable <= 0)
            {
                continue;
            }

            var pileQuantity = Math.Min(spaceAvailable, remaining);
            var index = Array.IndexOf(Container, stack);
            Container[index].Quantity += pileQuantity;
            remaining -= pileQuantity;

            if (remaining <= 0)
            {
                break;
            }
        }

        return remaining;
    }
}
