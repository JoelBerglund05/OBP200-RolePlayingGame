using OBP200_RolePlayingGame.Context.Combat;

namespace OBP200_RolePlayingGame.Character;

public interface IAttacker
{
    int CalculateDamage(Random random);
}