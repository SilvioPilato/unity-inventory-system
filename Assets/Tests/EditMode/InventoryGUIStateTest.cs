using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tests.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class InventoryGUIStateTest
    {
        [Test]
        public void CanGetAListOfInventorySlots()
        {
            InventoryGUIState state = An.InventoryGUIState;
            Assert.IsInstanceOf<List<InventorySlot>>(state.InventorySlots);
        }
        
        [Test]
        public void CanGetAListOfSlotsOfSetSize()
        {
            const int size = 5;
            InventoryGUIState state = An.InventoryGUIState.WithSize(size);
            Assert.IsInstanceOf<List<InventorySlot>>(state.InventorySlots);
            Assert.That(state.InventorySlots, Has.Exactly(size).TypeOf<InventorySlot>());
        }

        [Test]
        public void CanGetSlotIndex()
        {
            const int size = 5;
            InventoryGUIState state = An.InventoryGUIState.WithSize(5);
            var slots = state.InventorySlots.Select(slot => slot.Index);
            Assert.That(slots, Is.Unique.And.All.TypeOf<int>().And.GreaterThanOrEqualTo(0).And.LessThan(size));
        }
    }
}