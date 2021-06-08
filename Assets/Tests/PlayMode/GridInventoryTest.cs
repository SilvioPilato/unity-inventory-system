using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GridInventoryTest
{
    [UnityTest]
    public IEnumerator GridInventoryCanGetGridGroup()
    {
        var go = new GameObject();
        go.AddComponent<GridInventory>();
        yield return null;
        var gridGroup = go.GetComponent<GridLayoutGroup>();
        Assert.IsNotNull(gridGroup);
    }
    
    [UnityTest]
    public IEnumerator GridInventoryCanGetSetupInventorySlot()
    {
        var size = 8;
        var go = new GameObject();
        var gridInventory = go.AddComponent<GridInventory>();
        gridInventory.inventoryContainer = ScriptableObject.CreateInstance<InventoryContainer>();
        gridInventory.inventoryContainer.Size = size;
        gridInventory.slotPrefab = new GameObject();
        gridInventory.SetupInventory();
        yield return null;
        var slots = go.transform.GetComponentsInChildren<InventorySlotGUI>();
        Assert.That(slots, Has.Exactly(size).TypeOf(typeof(InventorySlotGUI)));
    }
}
