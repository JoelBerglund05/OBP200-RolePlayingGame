using OBP200_RolePlayingGame.Character;

namespace OBP200_RolePlayingGame.Player.PlayableClassTypes;

public class Mage : Player
{
    public Mage(string name) : base()
    {
        int maxhp = 28;
        int attack = 10;
        int defense = 2;
        int gold = 15;
        int xp = 0;
        
        stats = new CharacterStats(maxhp, attack, defense, xp, gold, name);
        
        
        Potions = 2;
        Buff = 2;
        ClassType = "Mage";
        Chance = 0.35;
    }

    public override int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
        // Fireball: stor skada, kostar guld
        if (stats.Gold >= 3)
        {
            Console.WriteLine("Mage kastar Fireball!");
            stats.RemoveGold(3);
            specialDmg = Math.Max(3, stats.Attack + 5);
        }
        else
        {
            Console.WriteLine("Inte tillräckligt med guld för att kasta Fireball (kostar 3).");
            specialDmg = 0;
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
            stats.AddMaxHp(4);
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}
