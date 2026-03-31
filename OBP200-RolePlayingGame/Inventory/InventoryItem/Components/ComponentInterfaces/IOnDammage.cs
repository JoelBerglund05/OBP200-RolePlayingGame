using OBP200_RolePlayingGame.Context.Combat;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem.Components.ComponentInterfaces;

public interface IOnDammage : IItemComponent
{
    void OnDammage(DammageContext context);
}