using System;
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

    public void SetupInventory()
    {
        var size = inventoryContainer.Size;
        for (var i = 0; i < size; i++)
        {
            var slotGo = Instantiate(slotPrefab,transform);
            slotGo.AddComponent<InventorySlotGUI>();
        }
    }

    public void UpdateInventory()
    {
        throw new System.NotImplementedException();
    }
}
