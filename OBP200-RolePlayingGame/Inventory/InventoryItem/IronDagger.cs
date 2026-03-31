using OBP200_RolePlayingGame.Inventory.InventoryItem.Components;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem;

public class IronDagger : Item
{
    public IronDagger()
    {
        name = "Iron Dagger";
        components.Add(new DamageComponent(3));
    }
}