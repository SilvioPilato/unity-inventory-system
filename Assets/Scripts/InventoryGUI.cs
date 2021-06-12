using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryGUI : MonoBehaviour
{
    public InventoryContainer inventoryContainer;
    public GameObject slotPrefab;
    public Vector2 iconSize = new Vector2(32, 32);
    public List<InventorySlotGUI> slots = new List<InventorySlotGUI>();

    public void Start()
    {
        SetupInventory();
    }

    private void UpdateInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var i = 0; i < size; i++)
        {
            UpdateSlot(slots[i]);
        }
    }

    private void SetupInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var index = 0; index < size; index++)
        { 
            CreateSlotAt(index);
            
        }
        UpdateInventory();
    }

    private void TearDownInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var i = 0; i < size; i++)
        {
            slots[i].OnItemDrop -= MoveItems;
            Destroy(slots[i].gameObject);
        }
    }

    private void CreateSlotAt(int index)
    {
        var slotGo = Instantiate(slotPrefab,transform);
        slotGo.name = $"InventorySlot{index}";
        var inventorySlotGUI = slotGo.AddComponent<InventorySlotGUI>();
        inventorySlotGUI.OnItemDrop+=MoveItems;
        inventorySlotGUI.index = index;
        
        var itemIcon = Instantiate(new GameObject(),slotGo.transform);
        var inventoryItemGUI = itemIcon.AddComponent<InventoryItemGUI>();
        inventoryItemGUI.CurrentSlotIndex = index;
        
        slots.Add(inventorySlotGUI);
    }

    private void UpdateSlot(InventorySlotGUI slot)
    {
        var slotGo = slot.gameObject;
        var slotIndex = slot.index;
        
        var stack = inventoryContainer.Container.Count > slotIndex 
            ? inventoryContainer.Container[slotIndex] 
            : null;
        var item = stack?.Item;
        
        if (item == null) return;
        
        var itemIcon = Instantiate(new GameObject(),slotGo.transform);
        var inventoryItemGUI = itemIcon.AddComponent<InventoryItemGUI>();
        inventoryItemGUI.Icon = item.Sprite;
        inventoryItemGUI.EnableIcon();
    }

    private static void MoveItems(int fromSlotIndex, int toSlotIndex)
    {
        Debug.Log($"Moved item from slot {fromSlotIndex} to slot {toSlotIndex}");
    }

    private void OnDestroy()
    {
        TearDownInventory();
    }
}
