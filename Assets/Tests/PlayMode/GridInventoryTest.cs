using System.Collections;
using NUnit.Framework;
using Tests.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GridInventoryTest
{
    [UnityTest]
    public IEnumerator CanGetGridGroup()
    {
        var go = new GameObject();
        go.AddComponent<GridInventory>();
        yield return null;
        var gridGroup = go.GetComponent<GridLayoutGroup>();
        Assert.IsNotNull(gridGroup);
    }
    
    [UnityTest]
    public IEnumerator CanGetSetupInventorySlots()
    {
        var go = new GameObject();
        var gridInventory = go.AddComponent<GridInventory>();
        gridInventory.inventoryContainer = An.InventoryContainer.WithSize(8);
        gridInventory.slotPrefab = new GameObject();
        yield return null;
        var slots = go.transform.GetComponentsInChildren<InventorySlotGUI>();
        Assert.That(slots, Has.Exactly(8).TypeOf(typeof(InventorySlotGUI)));
    }
}
