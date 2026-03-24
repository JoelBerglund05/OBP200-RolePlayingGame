namespace OBP200_RolePlayingGame;

public class Warrior : Player
{
    public Warrior(string name) 
        : base(name)
    {
        Maxhp = 40;
        Hp = Maxhp;
        Attack = 7;
        Defense = 5;
        Potions = 2;
        Gold = 15;
        Buff = 1;
        ClassType = "Warrior";
        Chance = 0.25;
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
        
        // Heavy Strike: hög skada men självskada
        Console.WriteLine("Warrior använder Heavy Strike!");
        specialDmg = Math.Max(2, Attack + 3 - enemyDefense);
        ApplyDamage(2); // självskada
        
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
            Level += 1;
            Maxhp += 6; Attack += 2; Defense += 2;
            Console.WriteLine($"Du når nivå {Level}! Värden ökade och HP återställd.");
        }
    }
}