using OBP200_RolePlayingGame.Character;
using OBP200_RolePlayingGame.Inventory.InventoryItem;

namespace OBP200_RolePlayingGame.Player;

public abstract class Player : IAddLoot, IAttacker, IDefender
{
    protected CharacterStats stats;
    
    protected Player()
    {
        Level = 1;
        Inventory = new Inventory.Inventory();
    }

    public int Potions
    {
        protected set;
        get;
    }
    
    public string ClassType
    {
        protected set;
        get;
    }

    public Inventory.Inventory Inventory
    {
        private set;
        get;
    }

    protected int Level
    {
        set;
        get;
    }
    
    protected int Buff { set; get; }

    protected double Chance { set; get; } = 0.25;

    public int GetGold()
    {
        return stats.Gold;
    }

    public void AddLoot(Item item)
    {
        Inventory.AddItem(item);
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public void Rest()
    {
        stats.ResetHp();
    }

    public virtual int CalculateDamage(Random Rng)
    {
        // Beräkna grundskada
        int baseDmg = Math.Max(1, stats.Attack);
        int roll = Rng.Next(0, 3); // liten variation

        baseDmg += Buff;
        
        return Math.Max(1, baseDmg + roll);
    }
    
    public abstract int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng);
    
    protected abstract void MaybeLevelUp();
    
    public bool TryRunAway(Random Rng)
    {
        return (Rng.NextDouble() < Chance);
    }
    
    public void UsePotion()
    {
        if (Potions <= 0)
        {
            Console.WriteLine("Du har inga drycker kvar.");
            return;
        }

        // Helning av spelaren
        int heal = 12;
        Potions -= 1;

        Console.WriteLine($"Du dricker en dryck och återfår {heal} HP.");
        stats.AddHp(heal);
    }
    
    public bool IsDead()
    {
        return stats.Hp <= 0;
    }

    public void AddXp(int amount)
    {
        stats.AddXp(amount);
        MaybeLevelUp();
    }

    public void AddGold(int amount)
    {
        stats.AddGold(amount);
    }
    
    public void TryBuy(int cost, ShopItem shopItem, string successMsg)
    {
        if (stats.Gold >= cost)
        {
            stats.RemoveGold(cost);
            switch (shopItem)
            {
                case ShopItem.Potion:
                    Potions += 1;
                    break;
                case ShopItem.Weapon:
                    // Attack += 2;
                    break;
                case ShopItem.Armor:
                    // Defense += 2;
                    break;
                Default:
                    break;
            }
            Console.WriteLine(successMsg);
        }
        else
        {
            Console.WriteLine("Du har inte råd.");
        }
    }
    
    public void ShowStatus()
    {
        Console.WriteLine($"[{stats.Name} | {ClassType}]  HP {stats.Hp}/{stats.MaxHp}  Attack {stats.Attack}  Defense {stats.Defense}  LVL {Level}  XP {stats.Xp}  Guld {stats.Gold}  Drycker {Potions}");
        Inventory.ViewInventory();
    }

    protected int NextLevelThreshold()
    {
        return Level == 1 ? 10 : (Level == 2 ? 25 : (Level == 3 ? 45 : Level * 20));
    }
}