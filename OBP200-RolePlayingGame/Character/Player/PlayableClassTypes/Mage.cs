namespace OBP200_RolePlayingGame.Player.PlayableClassTypes;

public class Mage : Player
{
    public Mage(string name) : base(name)
    {
        Maxhp = 28;
        Hp = Maxhp;
        Attack = 10;
        Defense = 2;
        Potions = 2;
        Gold = 15;
        Buff = 2;
        ClassType = "Mage";
        Chance = 0.35;
    }
    
    public override int CalculateDamage(int enemyDefense, Random Rng)
    {
        // Beräkna grundskada
        int baseDmg = Math.Max(1, Attack - (enemyDefense / 2));
        int roll = Rng.Next(0, 3); // liten variation
        
        baseDmg += Buff;
        
        return Math.Max(1, baseDmg + roll);
    }

    public override int UseClassSpecial(int enemyDefense, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
        // Fireball: stor skada, kostar guld
        if (Gold >= 3)
        {
            Console.WriteLine("Mage kastar Fireball!");
            Gold -= 3;
            specialDmg = Math.Max(3, Attack + 5 - (enemyDefense / 2));
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
        if (Xp >= NextLevelThreshold())
        {
            Level++;
            Maxhp += 4; Attack += 4; Defense += 1;
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}
