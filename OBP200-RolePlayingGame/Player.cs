namespace OBP200_RolePlayingGame;

public abstract class Player
{
    protected Player(string name)
    {
        Name = name;
        Xp = 0;
        Level = 1;
        Inventory = "Wooden Sword;Cloth Armor";
    }

    public int Def
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
    
    public string Cls
    {
        protected set;
        get;
    }

    public string Inventory
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

    protected int Atk
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
    

    public void Rest()
    {
        Hp = Maxhp;
    }

    public abstract int CalculateDamage(int enemyDef, Random Rng);

    public abstract int UseClassSpecial(int enemyDef, bool vsBoss, Random Rng);
    
    public abstract bool TryRunAway(Random Rng);
    
    protected abstract void MaybeLevelUp();
    
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

    public void AddToInventory(string item)
    {
        var inv = (Inventory ?? "").Trim();
        Inventory = string.IsNullOrEmpty(inv) ? item : (inv + ";" + item);
    }

    public void ReplaceInventory(List<string> items)
    {
        items = items.Where(x => x != "Minor Gem").ToList();
        Inventory = items.Count == 0 ? "" : string.Join(";", items);
    }
    
    public void TryBuy(int cost, Item item, string successMsg)
    {
        if (Gold >= cost)
        {
            Gold -= cost;
            switch (item)
            {
                case Item.potion:
                    Potions += 1;
                    break;
                case Item.wepon:
                    Atk += 1;
                    break;
                case Item.armor:
                    Def += 2;
                    break;
                default:
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
        Console.WriteLine($"[{Name} | {Cls}]  HP {Hp}/{Maxhp}  ATK {Atk}  DEF {Def}  LVL {Level}  XP {Xp}  Guld {Gold}  Drycker {Potions}");
        if (!string.IsNullOrWhiteSpace(Inventory))
        {
            Console.WriteLine($"Väska: {Inventory}");
        }
    }

    protected int NextLevelThreshold()
    {
        return Level == 1 ? 10 : (Level == 2 ? 25 : (Level == 3 ? 45 : Level * 20));
    }
}