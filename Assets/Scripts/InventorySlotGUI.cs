using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotGUI: MonoBehaviour, IDropHandler
{
    public int index = -1;
    public event Action<int, int> OnItemDrop;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("item dropped", this);
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemGUI>();
        if (inventoryItem == null) return;
        OnItemDrop?.Invoke(inventoryItem.CurrentSlotIndex, index);
    }
}
