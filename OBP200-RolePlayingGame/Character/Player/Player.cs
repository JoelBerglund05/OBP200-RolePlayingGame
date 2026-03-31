namespace OBP200_RolePlayingGame.Player;

public abstract class Player
{
    protected Player(string name)
    {
        Name = name;
        Xp = 0;
        Level = 1;
        Inventory = new Inventory.Inventory();
    }

    public int Defense
    {
        protected set;
        get;
    }

    public int Gold
    {
        protected set;
        get;
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
        protected set;
        get;
    }
    
    private string Name
    {
        set;
        get;
    }
    
    protected int Hp
    {
        set;
        get;
    }
    
    protected int Maxhp
    {
        set;
        get;
    }

    protected int Attack
    {
        set;
        get;
    }
    
    protected int Xp
    {
        set;
        get;
    }

    protected int Level
    {
        set;
        get;
    }
    
    protected int Buff { set; get; }

    protected double Chance { set; get; } = 0.25;
    

    public void Rest()
    {
        Hp = Maxhp;
    }

    public abstract int CalculateDamage(int enemyDefense, Random Rng);

    public abstract int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng);
    
    protected abstract void MaybeLevelUp();
    
    public bool TryRunAway(Random Rng)
    {
        return (Rng.NextDouble() < Chance);
    }
    
    public void ApplyDamage(int dmg)
    {
        Hp -= Math.Max(0, dmg);
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
        int newHp = Math.Min(Maxhp, Hp + heal);
        Potions -= 1;

        Console.WriteLine($"Du dricker en dryck och återfår {newHp - Hp} HP.");
        Hp = newHp;
    }
    
    public bool IsDead()
    {
        return Hp <= 0;
    }

    public void AddXp(int amount)
    {
        Xp += Math.Max(0, amount);
        MaybeLevelUp();
    }

    public void AddGold(int amount)
    {
        Gold += Math.Max(0, amount);
    }
    
    public void TryBuy(int cost, ShopItem shopItem, string successMsg)
    {
        if (Gold >= cost)
        {
            Gold -= cost;
            switch (shopItem)
            {
                case ShopItem.Potion:
                    Potions += 1;
                    break;
                case ShopItem.Weapon:
                    Attack += 2;
                    break;
                case ShopItem.Armor:
                    Defense += 2;
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
        Console.WriteLine($"[{Name} | {ClassType}]  HP {Hp}/{Maxhp}  Attack {Attack}  Defense {Defense}  LVL {Level}  XP {Xp}  Guld {Gold}  Drycker {Potions}");
        Inventory.ViewInventory();
    }

    protected int NextLevelThreshold()
    {
        return Level == 1 ? 10 : (Level == 2 ? 25 : (Level == 3 ? 45 : Level * 20));
    }
}