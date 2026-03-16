namespace OBP200_RolePlayingGame;

public class Player
{
    public Player(string name, string cls, int hp, int maxhp, int atk, int def, int gold, int xp, int level,
        int potions, string inventory)
    {
        Name = name;
        Cls = cls ?? "Warrior";
        Hp = hp;
        Maxhp = maxhp;
        Atk = atk;
        Def = def;
        Gold = gold;
        Xp = xp;
        Level = level;
        Potions = potions;
        Inventory = inventory;
    }
    
    public string Name
    {
        private set;
        get;
    }
    
    public int Hp
    {
        private set;
        get;
    }
    
    public int Maxhp
    {
        private set;
        get;
    }

    public int Atk
    {
        private set;
        get;
    }

    public int Def
    {
        private set;
        get;
    }

    public int Gold
    {
        private set;
        get;
    }

    public int Potions
    {
        private set;
        get;
    }
    
    public int Xp
    {
        private set;
        get;
    }

    public int Level
    {
        private set;
        get;
    }

    public string Cls
    {
        private set;
        get;
    }

    public string Inventory
    {
        private set;
        get;
    }

    public void Rest()
    {
        Hp = Maxhp;
    }
    
    public int CalculateDamage(int enemyDef, Random Rng)
    {

        // Beräkna grundskada
        int baseDmg = Math.Max(1, Atk - (enemyDef / 2));
        int roll = Rng.Next(0, 3); // liten variation

        switch (Cls.Trim())
        {
            case "Warrior":
                baseDmg += 1; // warrior buff
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
    
    public int UseClassSpecial(int enemyDef, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;

        // Hantering av specialförmågor
        if (Cls == "Warrior")
        {
            // Heavy Strike: hög skada men självskada
            Console.WriteLine("Warrior använder Heavy Strike!");
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
    
    public void MaybeLevelUp()
    {
        // Nivåtrösklar
        int nextThreshold = Level == 1 ? 10 : (Level == 2 ? 25 : (Level == 3 ? 45 : Level * 20));

        if (Xp >= nextThreshold)
        {
            Level += 1;

            // Uppgradering baserad på karaktärsklass

            switch (Cls)
            {
                case "Warrior":
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
}