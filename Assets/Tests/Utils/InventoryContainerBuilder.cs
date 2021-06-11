using UnityEngine;

namespace Tests.Utils
{
    public class InventoryContainerBuilder
    {
        private ItemStack[] _stacks = new ItemStack[0];
        private int _size;
        public InventoryContainerBuilder WithItems(params ItemStack[] stacks)
        {
            _stacks = stacks;
            return this;
        }
        public InventoryContainerBuilder WithSize(int size)
        {
            _size = size;
            return this;
        }
        private InventoryContainer Build()
        {
            var container = ScriptableObject.CreateInstance<InventoryContainer>();
            container.Size = _size;
            
            foreach (var stack in _stacks)
            {
                container.AddNew(stack);
            }
            _stacks = new ItemStack[0];
            _size = 0;

            return container;
        }

        public static implicit operator InventoryContainer(InventoryContainerBuilder builder) => builder.Build();
    }
}