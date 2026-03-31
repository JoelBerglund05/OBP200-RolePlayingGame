using System.ComponentModel;
using OBP200_RolePlayingGame.Context.Combat;
using OBP200_RolePlayingGame.Inventory.InventoryItem.Components.ComponentInterfaces;

namespace OBP200_RolePlayingGame.Inventory.InventoryItem;

public class DamageComponent : IOnHit
{
    private int Damage { get; set; }

    public DamageComponent(int damage)
    {
        this.Damage = damage;
    }
    
    public void OnHit(HitContext context)
    {
        context.AddDamage(Damage);
    }
}