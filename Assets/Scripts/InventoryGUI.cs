using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryGUI : MonoBehaviour
{
    public InventoryContainer inventoryContainer;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public Vector2 iconSize = new Vector2(32, 32);
    public InventorySlotGUI[] slots;
    public InventoryItemGUI[] items;
    
    private void UpdateInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var i = 0; i < size; i++)
        {
            UpdateItemAt(i);
        }
    }

    private void SetupInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var index = 0; index < size; index++)
        { 
            CreateSlotAt(index);
            SetupSlot(slots[index]);
            UpdateItemAt(index);
        }
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
        var inventorySlotGUI = slotGo.GetComponent<InventorySlotGUI>();
        inventorySlotGUI.OnItemDrop+=MoveItems;
        inventorySlotGUI.index = index;
        slots[index] = inventorySlotGUI;
    }

    private void SetupSlot(InventorySlotGUI slot)
    {
        var slotGo = slot.gameObject;
        var slotIndex = slot.index;
        var itemIcon = Instantiate(itemPrefab,slotGo.transform);
        var inventoryItemGUI = itemIcon.GetComponent<InventoryItemGUI>();
        if (inventoryItemGUI == null) return;
        inventoryItemGUI.OnClick += OnItemClick;
        inventoryItemGUI.IconSize = iconSize;
        inventoryItemGUI.CurrentSlotIndex = slotIndex;
        items[slotIndex] = inventoryItemGUI;
    }

    private void UpdateItemAt(int i)
    {
        var stack = inventoryContainer.Size > i 
            ? inventoryContainer.Container[i] 
            : null;
        var item = stack?.Item;
        if (item == null)
        {
            items[i].Icon = null;
            items[i].DisableIcon();
            slots[i].DisableText();
            return;
        }
        items[i].Icon = item.Sprite;
        slots[i].SetText(stack.Quantity.ToString());
        slots[i].EnableText();
        items[i].EnableIcon();
    }
    

    private void MoveItems(int fromSlotIndex, int toSlotIndex)
    {
        if (!IsIndexInSlotBounds(fromSlotIndex) || !IsIndexInSlotBounds(toSlotIndex)) return;
        var stack = inventoryContainer.Container[fromSlotIndex];
        var remaining = inventoryContainer.AddAt(toSlotIndex, stack);
        inventoryContainer.Remove(fromSlotIndex, stack.Quantity - remaining);
        UpdateInventory();
    }

    private bool IsIndexInSlotBounds(int index)
    {
        return index < slots.Length && index >= 0;
    }

    private void Awake()
    {
        slots = new InventorySlotGUI[inventoryContainer.Size];
        items = new InventoryItemGUI[inventoryContainer.Size];
    }

    private void OnEnable()
    {
        SetupInventory();
    }

    private void OnDisable()
    {
        TearDownInventory();
    }

    private void OnItemClick(InventoryItemGUI itemGUI)
    {
        var index = itemGUI.CurrentSlotIndex;
        if (inventoryContainer.Size < index) return;
        var item = inventoryContainer.Container[index]?.Item;
        if (item == null) return;
        item.Use();
    }
}
