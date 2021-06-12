using System.Collections.Generic;

public class InventoryGUIState
{
    public readonly List<InventorySlot> InventorySlots = new List<InventorySlot>();

    public InventoryGUIState(int size = 0)
    {
        SetNewInventory(size);
    }

    private void SetNewInventory(int size)
    {
        for (var i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlot {Index = i});
        }
    }
}
