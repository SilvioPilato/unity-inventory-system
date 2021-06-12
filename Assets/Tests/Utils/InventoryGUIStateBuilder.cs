namespace Tests.Utils
{
    public class InventoryGUIStateBuilder
    {
        private int _size;
        public InventoryGUIStateBuilder WithSize(int size)
        {
            _size = size;
            return this;
        }
        
        private InventoryGUIState Build()
        {
            return new InventoryGUIState(_size);
        }

        public static implicit operator InventoryGUIState(InventoryGUIStateBuilder builder) => builder.Build();
    }
}