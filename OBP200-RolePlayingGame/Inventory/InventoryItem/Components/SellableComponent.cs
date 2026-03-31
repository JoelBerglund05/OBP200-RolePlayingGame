using OBP200_RolePlayingGame.Inventory.InventoryItem.Components.ComponentInterfaces;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem.Components;

public class SellableComponent : ISellable
{
    private int Value { get; set; }
    
    public SellableComponent(int value)
    {
        this.Value = value;
    }
    
    public int GetValue()
    {
        return Value;
    }
}