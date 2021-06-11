using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDropHandler : MonoBehaviour, IDropHandler
{
    public event Action<int> OnItemDrop;
    public void OnDrop(PointerEventData eventData)
    {
        var invPanel = transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, eventData.position)) return;
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemGUI>();
        if (inventoryItem == null) return;
        OnItemDrop?.Invoke(inventoryItem.CurrentSlotIndex);
    }
}
