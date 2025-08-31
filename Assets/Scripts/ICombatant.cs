using System.Collections.Generic;

public interface ICombatant
{
    float MP { get; }
    float SPD { get; }
    float ATK { get; }
    float DEF { get; }

    bool IsAlive();
    void TakeDamage(float damage);
    List<IAttack> Attacks { get; }   
    bool Attack(ICombatant target);
}
