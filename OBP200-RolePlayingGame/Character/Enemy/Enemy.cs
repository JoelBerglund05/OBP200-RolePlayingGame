using OBP200_RolePlayingGame.Inventory.InventoryItem;
using OBP200_RolePlayingGame.Player;

namespace OBP200_RolePlayingGame.Character.Enemy;

public class Enemy : IAttacker, IDefender
{
    private CharacterStats stats;
    
    public string Race { get; private set; }

    public Enemy(int maxHp, int attack, int defense, int xp, int gold, string race, string name, Random random)
    {
        stats = new CharacterStats(
            maxHp + random.Next(-1, 3), 
            attack + random.Next(0, 2), 
            defense + random.Next(0, 2), 
            xp + random.Next(0, 3), 
            gold + random.Next(0, 3), 
            name
        );
        
        Race = race;
    }

    public int CalculateDamage(Random random)
    {
        int roll = random.Next(0, 3);
        int damage = Math.Max(1, stats.Attack) + roll;
        
        damage = GlancingBlow(random, damage);
        
        return damage;
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }
    
    public void MaybeDropLoot(IAddLoot playerInventory, Random random)
    {
        // Enkel loot-regel
        if (random.NextDouble() < 0.35)
        {
            Item item = new MinorGem();
            // if (enemyName.Contains("Urdraken")) item = "Dragon Scale";

            playerInventory.AddLoot(item);

            Console.WriteLine($"Föremål hittat: {item.name} (lagt i din väska)");
        }
    }

    public int GetGoldReward()
    {
        return stats.Gold;
    }
    
    public int GetXpReward()
    {
        return stats.Xp;
    }

    public int GetHp()
    {
        return stats.Hp;
    }

    public int GetDefense()
    {
        return stats.Defense;
    }

    public int GetAttack()
    {
        return stats.Attack;
    }

    public string GetName()
    {
        return stats.Name;
    }

    private int GlancingBlow(Random random, int damage)
    {
        if (random.NextDouble() < 0.1) 
            return Math.Max(1, damage - 2);
        
        return damage;
    }
}