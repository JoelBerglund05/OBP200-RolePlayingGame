using OBP200_RolePlayingGame.Character;

namespace OBP200_RolePlayingGame.Player.PlayableClassTypes;

public class Rogue : Player
{
    public Rogue(string name) : base()
    {
        int maxhp = 32;
        int attack = 8;
        int defense = 3;
        int gold = 20;
        int xp = 0;
        
        stats = new CharacterStats(maxhp, attack, defense, xp, gold, name);
        
        Potions = 3;
        Buff = 0;
        ClassType = "Rogue";
        Chance = 0.5;
    }

    public override int CalculateDamage(Random Rng)
    {
        // Beräkna grundskada
        int baseDmg = Math.Max(1, stats.Attack);
        int roll = Rng.Next(0, 3); // liten variation
        
        baseDmg += (Rng.NextDouble() < 0.2) ? 4 : 0; // rogue crit-chans
        
        return Math.Max(1, baseDmg + roll);
    }

    public override int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
        // Backstab: chans att ignorera försvar, hög risk/hög belöning
        if (Rng.NextDouble() < 0.5)
        {
            Console.WriteLine("Rogue utför en lyckad Backstab!");
            specialDmg = Math.Max(4, stats.Attack + 6);
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
    
    protected override void MaybeLevelUp()
    {
        if (stats.Xp >= NextLevelThreshold())
        {
            Level++;
            stats.AddMaxHp(5);
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}
