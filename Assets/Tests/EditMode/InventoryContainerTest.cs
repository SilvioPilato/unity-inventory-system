using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class InventoryContainerTest
    {
        [Test]
        public void InventoryHasListOfItemStack()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            Assert.NotNull(inventory.Container);
            CollectionAssert.AllItemsAreInstancesOfType(inventory.Container, typeof(ItemStack));
        }

        [Test]
        public void InventoryHasSizeProperty()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            Assert.IsNotNull(inventory.Size);
            Assert.GreaterOrEqual(0, inventory.Size);
        }

        [Test]
        public void InventoryCanAddItem()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item);
            var index = inventory.AddNew(itemStack);
            Assert.Contains(itemStack, inventory.Container);
            Assert.AreEqual(0, index);
        }

        [Test]
        public void InventoryCanRemoveItem()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item);
            inventory.AddNew(itemStack);
            inventory.Remove(0);
            Assert.IsEmpty(inventory.Container); 
        }

        [Test]
        public void InventoryCanRemoveItemAmount()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item) {Quantity = 3, MaxStackSize = 3};
            inventory.AddNew(itemStack);
            var removed = inventory.Remove(0, 2);
            
            Assert.Contains(itemStack, inventory.Container);
            Assert.AreEqual(1, inventory.Container[0].Quantity);
            
            Assert.IsInstanceOf<ItemStack>(removed);
            Assert.NotNull(removed);
            Assert.AreSame(item, removed.Item);
            Assert.AreEqual(2, removed.Quantity);
            Assert.AreEqual(itemStack.MaxStackSize, removed.MaxStackSize);
        }
        
        [Test]
        [TestCase(3,3)]
        [TestCase(2,4)]
        public void InventoryCanRemoveWholeItemAmount(int quantityStored, int quantityRemoved)
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item) {Quantity = quantityStored, MaxStackSize = 3};
            inventory.AddNew(itemStack);
            var removed = inventory.Remove(0, quantityRemoved);
            
            Assert.IsEmpty(inventory.Container); 
            Assert.IsInstanceOf<ItemStack>(removed);
            Assert.NotNull(removed);
            Assert.AreSame(item, removed.Item);
            Assert.AreEqual(Math.Min(quantityRemoved, quantityStored), removed.Quantity);
            Assert.AreEqual(itemStack.MaxStackSize, removed.MaxStackSize);
        }
        
        [Test]
        public void InventoryIgnoresItemIndexOverflow()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item) {Quantity = 3, MaxStackSize = 3};
            inventory.AddNew(itemStack);
            var removed = inventory.Remove(1, 3);
            
            Assert.Contains(itemStack, inventory.Container);
            Assert.AreEqual(3, inventory.Container[0].Quantity);
            Assert.IsNull(removed);
        }

        [Test]
        public void InventoryCannotAddPastSizeLimit()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item1 = ScriptableObject.CreateInstance<Item>();
            var item2 = ScriptableObject.CreateInstance<Item>();
            var itemStack1 = new ItemStack(item1) {Quantity = 3, MaxStackSize = 3};
            var itemStack2 = new ItemStack(item2) {Quantity = 3, MaxStackSize = 3};
            inventory.AddNew(itemStack1);
            inventory.AddNew(itemStack2);
            
            Assert.Contains(itemStack1, inventory.Container);
            Assert.That(inventory.Container, Has.No.Member(itemStack2));
        }

        [Test]
        public void InventoryCanStashOnTopOfStack()
        {
            const int stack1Quantity = 1;
            const int stack2Quantity = 2;
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack1 = new ItemStack(item) {Quantity = stack1Quantity, MaxStackSize = 3};
            var itemStack2 = new ItemStack(item) {Quantity = stack2Quantity, MaxStackSize = 3};
            
            inventory.AddNew(itemStack1);
            var remaining = inventory.Stash(itemStack2);
            
            Assert.Contains(itemStack1, inventory.Container);
            Assert.AreEqual(stack1Quantity + stack2Quantity, itemStack1.Quantity);
            Assert.AreEqual(0, remaining);
        }

        [Test]
        public void InventoryEmptyCanStash()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item) {Quantity = 3, MaxStackSize = 3};
            
            var remaining = inventory.Stash(itemStack);
            
            Assert.Contains(itemStack, inventory.Container);
            Assert.AreEqual(1, inventory.Container.Count);
            Assert.AreEqual(0, remaining);
        }
        
        [Test]
        public void InventoryFullCannotStash()
        {
            var inventory = ScriptableObject.CreateInstance<InventoryContainer>();
            inventory.Size = 1;
            
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack1 = new ItemStack(item) {Quantity = 3, MaxStackSize = 3};
            var itemStack2 = new ItemStack(item) {Quantity = 3, MaxStackSize = 3};
            
            inventory.Stash(itemStack1);
            var remaining = inventory.Stash(itemStack2);
            
            Assert.Contains(itemStack1, inventory.Container);
            Assert.AreEqual(1, inventory.Container.Count);
            Assert.That(inventory.Container, Has.No.Member(itemStack2));
            Assert.AreEqual(itemStack2.Quantity, remaining);
        }
    }
}