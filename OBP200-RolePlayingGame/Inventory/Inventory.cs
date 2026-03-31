using OBP200_RolePlayingGame.Inventory.InventoryItem;
using OBP200_RolePlayingGame.Inventory.InventoryItem.Components;
using OBP200_RolePlayingGame.Inventory.InventoryItem.Components.ComponentInterfaces;

namespace OBP200_RolePlayingGame.Inventory;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        this.items = new List<Item>
        {
            new IronDagger(),
        };
    }
    
    public void AddItem(Item item)
    {
       items.Add(item);
    }

    public void RemoveItem(Item item)
    {
       items.Remove(item);
    }

    public int RemoveItems<T>() where T : Item
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item.GetType() == typeof(T))
            {
                items.Remove(item);
                count++;
            }
        }
        return count;
    }

    public List<Item> GetItems<T>() where T : IItemComponent
    {
        List<Item> items = new List<Item>();
        foreach (var item in this.items)
        {
            if(item.GetComponents<T>().Count() > 0)
                items.Add(item);
        }
        return items;
    }

    public void ViewInventory()
    {
        if (items.Count == 0)
            Console.WriteLine("Väskan är tom");
        else
        {
            Console.Write("Väskan innehåller: ");
            foreach (var item in items)
            {
                Console.Write($"{item.name} ");
            }

            Console.WriteLine();
        }
    }
}