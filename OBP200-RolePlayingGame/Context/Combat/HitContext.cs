using OBP200_RolePlayingGame.Character;

namespace OBP200_RolePlayingGame.Context.Combat;

public class HitContext : IDamageContext
{
    private IAttacker attacker; 
    private IDefender defender;
    private int damage = 0;

    public HitContext(IAttacker attacker, IDefender defender)
    {
        this.attacker = attacker;
        this.defender = defender;
    }

    public void AddDamage(int amount)
    {
        this.damage += Math.Max(0, amount);
    }
}