using OBP200_RolePlayingGame.Context.Combat;

namespace OBP200_RolePlayingGame.Character;

public interface IAttacker
{
    int Attack { get; set; }
    
    void CalculateDamage(Random random);
}