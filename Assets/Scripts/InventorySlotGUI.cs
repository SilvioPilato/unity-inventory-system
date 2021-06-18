using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotGUI: MonoBehaviour, IDropHandler
{
    public int index = -1;
    public event Action<int, int> OnItemDrop;
    [SerializeField]
    private InventorySlotTextGUI slotTextGUI;
    public void OnDrop(PointerEventData eventData)
    {
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemGUI>();
        if (inventoryItem == null) return;
        OnItemDrop?.Invoke(inventoryItem.CurrentSlotIndex, index);
    }

    public void SetText(string text)
    {
        if (slotTextGUI == null) return;
        slotTextGUI.SetText(text);
    }
}
