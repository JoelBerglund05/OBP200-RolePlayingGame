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
    
    public string Name
    {
        protected set;
        get;
    }
    
    public int Hp
    {
        protected set;
        get;
    }
    
    public int Maxhp
    {
        protected set;
        get;
    }

    public int Atk
    {
        protected set;
        get;
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
    
    public int Xp
    {
        protected set;
        get;
    }

    public int Level
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
    
    protected int Buff { set; get; }
    

    public void Rest()
    {
        Hp = Maxhp;
    }

    public virtual int CalculateDamage(int enemyDef, Random Rng)
    {

        // Beräkna grundskada
        int baseDmg = Math.Max(1, Atk - (enemyDef / 2));
        int roll = Rng.Next(0, 3); // liten variation

        switch (Cls.Trim())
        {
            case "DummaDIG":
                baseDmg += 1; // DummaDIG buff
                break;
            case "Mage":
                baseDmg += 2; // mage buff
                break;
            case "Rogue":
                baseDmg += (Rng.NextDouble() < 0.2) ? 4 : 0; // rogue crit-chans
                break;
            default:
                baseDmg += 0;
                break;
        }

        return Math.Max(1, baseDmg + roll);
    }
    
    public virtual int UseClassSpecial(int enemyDef, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;

        // Hantering av specialförmågor
        if (Cls == "DummaDIG")
        {
            // Heavy Strike: hög skada men självskada
            Console.WriteLine("DummaDIG använder Heavy Strike!");
            specialDmg = Math.Max(2, Atk + 3 - enemyDef);
            ApplyDamage(2); // självskada
        }
        else if (Cls == "Mage")
        {
            // Fireball: stor skada, kostar guld
            if (Gold >= 3)
            {
                Console.WriteLine("Mage kastar Fireball!");
                Gold -= 3;
                specialDmg = Math.Max(3, Atk + 5 - (enemyDef / 2));
            }
            else
            {
                Console.WriteLine("Inte tillräckligt med guld för att kasta Fireball (kostar 3).");
                specialDmg = 0;
            }
        }
        else if (Cls == "Rogue")
        {
            // Backstab: chans att ignorera försvar, hög risk/hög belöning
            if (Rng.NextDouble() < 0.5)
            {
                Console.WriteLine("Rogue utför en lyckad Backstab!");
                specialDmg = Math.Max(4, Atk + 6);
            }
            else
            {
                Console.WriteLine("Backstab misslyckades!");
                specialDmg = 1;
            }
        }
        else
        {
            specialDmg = 0;
        }

        // Dämpa skada mot bossen
        if (vsBoss)
        {
            specialDmg = (int)Math.Round(specialDmg * 0.8);
        }

        return Math.Max(0, specialDmg);
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
    
    public bool TryRunAway(Random Rng)
    {
        // Flyktschans baserad på karaktärsklass
        double chance = 0.25;
        if (Cls == "Rogue") chance = 0.5;
        if (Cls == "Mage") chance = 0.35;
        return Rng.NextDouble() < chance;
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
    
    public virtual void MaybeLevelUp()
    {
        if (Xp >= NextLevelThreshold())
        {
            Level += 1;

            // Uppgradering baserad på karaktärsklass

            switch (Cls)
            {
                case "DummaDIG":
                    Maxhp += 6; Atk += 2; Def += 2;
                    break;
                case "Mage":
                    Maxhp += 4; Atk += 4; Def += 1;
                    break;
                case "Rogue":
                    Maxhp += 5; Atk += 3; Def += 1;
                    break;
                default:
                    Maxhp += 4; Atk += 3; Def += 1;
                    break;
            }

            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
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