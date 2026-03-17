namespace OBP200_RolePlayingGame;

public class Rogue : Player
{
    public Rogue(string name) : base(name)
    {
        Maxhp = 32;
        Hp = Maxhp;
        Atk = 8;
        Def = 3;
        Potions = 3;
        Gold = 20;
        Buff = 0;
        Cls = "Rogue";
    }

    public override int CalculateDamage(int enemyDef, Random Rng)
    {
        // Beräkna grundskada
        int baseDmg = Math.Max(1, Atk - (enemyDef / 2));
        int roll = Rng.Next(0, 3); // liten variation
        
        baseDmg += (Rng.NextDouble() < 0.2) ? 4 : 0; // rogue crit-chans
        return Math.Max(1, baseDmg + roll);
    }

    public override int UseClassSpecial(int enemyDef, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
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
        
        // Dämpa skada mot bossen
        if (vsBoss)
        {
            specialDmg = (int)Math.Round(specialDmg * 0.8);
        }

        return Math.Max(0, specialDmg);
    }

    public override bool TryRunAway(Random Rng)
    {
        double chance = 0.5; 
        return (Rng.NextDouble() < chance);
    }

    protected override void MaybeLevelUp()
    {
        if (Xp >= NextLevelThreshold())
        {
            Level++;
            Maxhp += 5; Atk += 3; Def += 1;
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}
