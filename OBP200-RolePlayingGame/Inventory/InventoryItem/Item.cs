using OBP200_RolePlayingGame.Inventory.InventoryItem.Components.ComponentInterfaces;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem;

public abstract class Item
{
    public string name { get; protected set; }
    protected List<IItemComponent> components = new List<IItemComponent>();

    public virtual IEnumerable<T> GetComponents<T>() where T : IItemComponent
    {
        return this.components.OfType<T>();
    }
}