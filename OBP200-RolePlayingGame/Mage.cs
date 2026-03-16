namespace OBP200_RolePlayingGame;

public class Mage : Player
{
    public Mage(string name) : base(name)
    {
        Maxhp = 28;
        Hp = Maxhp;
        Atk = 10;
        Def = 2;
        Potions = 2;
        Gold = 15;
        Buff = 2;
        Cls = "Mage";
    }

    public override int CalculateDamage(int enemyDef, Random Rng)
    {
        // Beräkna grundskada
        int baseDmg = Math.Max(1, Atk - (enemyDef / 2));
        int roll = Rng.Next(0, 3); // liten variation
        
        baseDmg += Buff;
        
        return Math.Max(1, baseDmg + roll);
    }

    public override int UseClassSpecial(int enemyDef, bool vsBoss, Random Rng)
    {
        int specialDmg = 0;
        
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
        
        // Dämpa skada mot bossen
        if (vsBoss)
        {
            specialDmg = (int)Math.Round(specialDmg * 0.8);
        }

        return Math.Max(0, specialDmg);
    }

    public override void MaybeLevelUp()
    {
        if (Xp >= NextLevelThreshold())
        {
            Level++;
            Maxhp += 4; Atk += 4; Def += 1;
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}