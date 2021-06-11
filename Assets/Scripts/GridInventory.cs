using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridInventory : MonoBehaviour
{
    public InventoryContainer inventoryContainer;
    public GameObject slotPrefab;
    public void Start()
    {
        SetupInventory();
    }

    private void SetupInventory()
    {
        var size = (inventoryContainer != null) ? inventoryContainer.Size : 0;
        for (var i = 0; i < size; i++)
        {
            var slotGo = Instantiate(slotPrefab,transform);
            slotGo.AddComponent<InventorySlotGUI>();
            var item = inventoryContainer.Container.Count > i ? inventoryContainer.Container[i].Item : null;
            if (item == null) continue;
            var itemIcon = Instantiate(new GameObject(),slotGo.transform);
            itemIcon.AddComponent<InventoryItemGUI>();
            var imageComponent = itemIcon.AddComponent<Image>();
            
            imageComponent.sprite = item.Sprite;
        }
    }

    public void UpdateInventory()
    {
        throw new System.NotImplementedException();
    }
}
