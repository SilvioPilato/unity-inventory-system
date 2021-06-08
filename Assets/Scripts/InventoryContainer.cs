using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Container")]
public class InventoryContainer: ScriptableObject
{
    private const int SlotIndexNone = -1;
    [SerializeField]
    private List<ItemStack> container = new List<ItemStack>();
    [SerializeField] 
    private int size;
    public List<ItemStack> Container => container;
    public int Size
    {
        get => size;
        set => size = value;
    }

    public int AddNew(ItemStack itemStack)
    {
        if (container.Count >= Size) return SlotIndexNone;
        container.Add(itemStack);
        return container.IndexOf(itemStack);
    }
    
    public int Stash(ItemStack itemStack)
    {
        var availableStacks = container.FindAll(
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
        if (itemIndex >= container.Count) return null;
        
        var stack = container[itemIndex];
        if (amount == 0 || amount >= stack.Quantity )
        {
            container.RemoveAt(itemIndex);
            return stack;
        }
        container[itemIndex].Quantity -= amount;
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
            var index = container.IndexOf(stack);
            container[index].Quantity += pileQuantity;
            remaining -= pileQuantity;

            if (remaining <= 0)
            {
                break;
            }
        }

        return remaining;
    }
}
