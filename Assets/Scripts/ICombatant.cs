public interface ICombatant
{
    float SPD { get; }
    bool IsAlive();
    void Attack(ICombatant target);
}
