using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class ItemTest
    {
        [Test]
        public void ItemHasEmptyProperties()
        {
            var item = ScriptableObject.CreateInstance<Item>();
            Assert.IsEmpty(item.DisplayName);
            Assert.IsEmpty(item.Description);
            Assert.Null(item.Sprite);
            Assert.Null(item.Prefab);
        }
    
        [Test]
        public void ItemPropertiesCanBeSet()
        {
            var item = ScriptableObject.CreateInstance<Item>();
            var displayName = "displayName";
            var description = "a very short description about the item";
            var gameObject = new GameObject();
            var sprite = gameObject.GetComponent<Sprite>();

            item.DisplayName = displayName;
            item.Description = description;
            item.Prefab = gameObject;
            item.Sprite = sprite;
        
            Assert.AreSame(displayName, item.DisplayName);
            Assert.AreSame(description, item.Description);
            Assert.AreSame(gameObject, item.Prefab);
            Assert.AreSame(sprite, item.Sprite);
        }
    }
}
