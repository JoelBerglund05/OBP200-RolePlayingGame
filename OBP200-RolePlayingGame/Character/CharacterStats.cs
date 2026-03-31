namespace OBP200_RolePlayingGame.Character;

public class CharacterStats
{
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Attack  { get; private set; }
    public int Defense  { get; private set; }
    public int Xp { get; private set; }
    public int Gold  { get; private set; }
    public string Name  { get; private set; }
    
    public CharacterStats(int maxHp, int attack,  int defense, int xp, int gold, string name)
    {
        Hp = maxHp;
        MaxHp = maxHp;
        Attack = attack;
        Defense = defense;
        Xp = xp;
        Gold = gold;
        if (!string.IsNullOrEmpty(name))
            Name = name;
        else
            Name = "Namnlös";
    }
    
    public void ResetHp()
    {
        Hp = MaxHp;
    }

    public void TakeDamage(int damage)
    {
        int defenseBuff = Defense / 2;
        Hp -= Math.Max(damage - defenseBuff, 0);
    }

    public void AddMaxHp(int maxHp)
    {
        MaxHp += Math.Max(maxHp, 0);
    }

    public void AddHp(int hp)
    {
        Hp = Math.Max(Hp + Math.Min(hp, 0), MaxHp);
    }
    
    public void AddGold(int gold)
    {
        Gold += Math.Max(gold, 0);
    }
    
    public void RemoveGold(int gold)
    {
        Gold -= Math.Max(gold, Gold);
    }

    public void AddXp(int xp)
    {
        Xp += Math.Max(xp, 0);
    }
    
}