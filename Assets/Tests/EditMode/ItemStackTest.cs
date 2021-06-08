using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class ItemStackTest
    {
        [Test]
        public void ItemStackHasDefaultProperties()
        {
            var item = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item);
            Assert.AreSame(item, itemStack.Item);
            Assert.AreEqual(1, itemStack.MaxStackSize);
            Assert.AreEqual(0, itemStack.Quantity);
        }
        
        [Test]
        public void ItemStackPropertiesCanBeSet()
        {
            var item1 = ScriptableObject.CreateInstance<Item>();
            var item2 = ScriptableObject.CreateInstance<Item>();
            var itemStack = new ItemStack(item1, 1, 2);
            
            Assert.AreSame(item1, itemStack.Item);
            Assert.AreEqual(2, itemStack.MaxStackSize);
            Assert.AreEqual(1, itemStack.Quantity);

            itemStack.Item = item2;
            itemStack.Quantity = 3;
            itemStack.MaxStackSize = 5;
            
            Assert.AreSame(item2, itemStack.Item);
            Assert.AreEqual(5, itemStack.MaxStackSize);
            Assert.AreEqual(3, itemStack.Quantity);
        }
    }
}
