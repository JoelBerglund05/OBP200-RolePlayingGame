using OBP200_RolePlayingGame.Character;

namespace OBP200_RolePlayingGame.Player.PlayableClassTypes;

public class Warrior : Player
{
    public Warrior(string name) 
        : base()
    {
        
        int maxhp = 40;
        int attack = 7;
        int defense = 5;
        int gold = 15;
        int xp = 0;
        
        stats = new CharacterStats(maxhp, attack, defense, xp, gold, name);
        
        Potions = 2;
        Buff = 1;
        ClassType = "Warrior";
        Chance = 0.25;
    }

    public override int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
        // Heavy Strike: hög skada men självskada
        Console.WriteLine("Warrior använder Heavy Strike!");
        specialDmg = Math.Max(2, stats.Attack + 3);
        TakeDamage(2); // självskada
        
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
            Level += 1;
            stats.AddMaxHp(6);
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}