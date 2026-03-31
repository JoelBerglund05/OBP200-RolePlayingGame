using OBP200_RolePlayingGame.Inventory.InventoryItem.Components;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem;

public class MinorGem : Item
{
    public MinorGem()
    {
        name = "Minor Gem";
        components.Add(new SellableComponent(5));
    }
}